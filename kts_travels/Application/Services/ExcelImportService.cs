using kts_travels.SharedServices.Application.Services.Interfaces;
using kts_travels.SharedServices.Domain.Entities;
using kts_travels.SharedServices.Infrastructure.Persistence;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace kts_travels.SharedServices.Application.Services
{
    public class ExcelImportService(AppDbContext context, ILogger<ExcelImportService> logger) : IExcelImportService
    {
        private readonly AppDbContext _context = context;
        private readonly ILogger<ExcelImportService> _logger = logger;


        public async Task ImportDataFromExcelAsync(Stream excelFile)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            try
            {
                using (var package = new ExcelPackage(excelFile))
                {
                    foreach (var sheet in package.Workbook.Worksheets)
                    {
                        if (sheet.Name.Contains("AVERAGE", StringComparison.OrdinalIgnoreCase))
                        {
                            _logger.LogInformation("Skipping average details sheet: {sheet.Name}", sheet.Name);
                            continue;
                        }

                        var rows = sheet.Dimension.Rows;

                        int location = sheet.Cells[1, 1].Text.Contains("BARODA", StringComparison.OrdinalIgnoreCase) ? 1 : 2;
                        _logger.LogInformation("Processing sheet: {sheet.Name}, Location: {location}", sheet.Name, location);
                        var existingSite = await _context.Sites.FirstOrDefaultAsync(s => s.SiteId == location);
                        if (existingSite == null)
                        {
                            _logger.LogWarning("Site with ID {location} does not exist. Skipping this location.", location);
                            continue;
                        }

                        for (int row = 3; row <= rows; row++)
                        {
                            var vehicleNo = sheet.Cells[row, 3].Text.Trim();

                            if (string.IsNullOrEmpty(vehicleNo))
                            {
                                _logger.LogWarning("Skipping row {row} due to empty VehicleNo.", row);
                                continue;
                            }

                            double? dieselLiters = null;
                            if (!string.IsNullOrEmpty(sheet.Cells[row, 4].Text))
                            {
                                dieselLiters = Convert.ToDouble(sheet.Cells[row, 4].Text);
                            }

                            int? startingKm = null;
                            if (!string.IsNullOrEmpty(sheet.Cells[row, 5].Text))
                            {
                                startingKm = Convert.ToInt32(sheet.Cells[row, 5].Text);
                            }

                            DateTime date = DateTime.ParseExact(sheet.Cells[row, 2].Text.Trim(), "dd-MM-yyyy", null);

                            var existingVehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.VehicleNo == vehicleNo);

                            if (existingVehicle == null)
                            {
                                var vehicle = new Vehicle
                                {
                                    VehicleNo = vehicleNo
                                };
                                await _context.Vehicles.AddAsync(vehicle);
                                await _context.SaveChangesAsync();
                                _logger.LogInformation("Added new vehicle: {vehicleNo}", vehicleNo);
                                existingVehicle = vehicle; // Update the reference to the newly created vehicle
                            }

                            // Create TripLog entry
                            var tripLog = new TripLog
                            {
                                VehicleId = existingVehicle.VehicleId, // Reference the VehicleId
                                Date = date,
                                DieselLiters = (decimal?)dieselLiters ?? 0,
                                StartingKm = startingKm ?? 0,
                                LocationId = location // Set the LocationId from the determined location
                            };

                            _context.TripLogs.Add(tripLog);
                            _logger.LogInformation("Added TripLog for vehicle {vehicleNo}: Date = {date}, DieselLiters = {dieselLiters}, StartingKm = {startingKm}", vehicleNo, date, dieselLiters, startingKm);
                        }
                    }
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("Imported vehicles and trip logs successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while importing vehicles from Excel.");
                throw; // Rethrow the exception after logging
            }
        }
    }
}
