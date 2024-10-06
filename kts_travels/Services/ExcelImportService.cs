using kts_travels.Data;
using kts_travels.Models;
using kts_travels.Models.Dto;
using kts_travels.Services.IServices;
using OfficeOpenXml;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;

namespace kts_travels.Services
{
    public class ExcelImportService : IExcelImportService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ExcelImportService> _logger;

        public ExcelImportService(AppDbContext context, ILogger<ExcelImportService> logger)
        {
            _context = context;
            _logger = logger;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<ResponseDto> ImportDataAsync(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return new ResponseDto { IsSuccess = false, Message = "Uploaded file is empty or null." };
            }

            if (!file.FileName.EndsWith(".xlsx"))
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid File Format. Please upload Excel file"
                };
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        if (worksheet == null)
                        {
                            return new ResponseDto
                            {
                                IsSuccess = false,
                                Message = "Excel file is empty"
                            };
                        }

                        var entries = new List<DieselEntry>();
                        for (int row = 2; row < worksheet.Dimension.Rows; row++)
                        {
                            try
                            {
                                var dateStr = worksheet.Cells[row, 2].GetValue<string>();
                                var date = DateTime.ParseExact(dateStr, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                                var vehicleNo = worksheet.Cells[row, 3].GetValue<string>();
                                var dieselLiters = worksheet.Cells[row, 4].GetValue<double>();
                                var startingKm = worksheet.Cells[row, 5].GetValue<int>();

                                var dieselEntry = new DieselEntry
                                {
                                    Location = "Baroda", // This is one is hardcoded bruh
                                    Date = date,
                                    VehicleNo = vehicleNo,
                                    DieselLiters = dieselLiters,
                                    StartingKm = startingKm
                                };

                                // You can add more complex validation here.
                                if (string.IsNullOrEmpty(dieselEntry.Location) || string.IsNullOrEmpty(dieselEntry.VehicleNo))
                                {
                                    _logger.LogWarning("Invalid entry at row {Row}", row);
                                    continue;
                                }

                                entries.Add(dieselEntry);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "Error parsing row {Row}", row);
                            }
                        }

                        // Save to database in bulk
                        if (entries.Any())
                        {
                            await _context.DieselEntries.AddRangeAsync(entries);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            return new ResponseDto { IsSuccess = false, Message = "No valid data found to import." };
                        }
                    }
                }

                return new ResponseDto { IsSuccess = true, Message = "Data imported successfully." };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while importing Excel file.");
                return new ResponseDto { IsSuccess = false, Message = "An error occurred while importing the file. Please check the logs for more details." };
            }
        }
    }
}
