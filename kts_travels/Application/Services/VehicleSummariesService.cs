using kts_travels.SharedServices.Application.Dtos;
using kts_travels.SharedServices.Application.Factories.Interfaces;
using kts_travels.SharedServices.Application.Services.Interfaces;
using kts_travels.SharedServices.Domain.Repositories;
using kts_travels.SharedServices.Domain.Entities;
using kts_travels.SharedServices.Infrastructure.Persistence.Repositories;
using AutoMapper;
using System.Drawing.Printing;

namespace kts_travels.SharedServices.Application.Services
{
    public class VehicleSummariesService : IVehicleSummariesService
    {
        private readonly IVehicleSummariesFactory _vehicleSummaryFactory;
        private readonly IVehicleSummariesRepository _vehicleSummaryRepository;
        private readonly IMapper _mapper;
        private readonly ITripLogRepository _triplogRepository;

#pragma warning disable IDE0290
        public VehicleSummariesService(IVehicleSummariesFactory vehicleSummaryFactory, IVehicleSummariesRepository vehicleSummaryRepository, IMapper mapper, ITripLogRepository triplogRepository)
        {
            _vehicleSummaryFactory = vehicleSummaryFactory;
            _vehicleSummaryRepository = vehicleSummaryRepository;
            _mapper = mapper;
            _triplogRepository = triplogRepository;
        }


        public async Task<ResponseDto> GetVehicleSummariesAsync()
        {
            try
            {
                var summaries = await _vehicleSummaryRepository.GetSummariesAsync();
                var vehicleSummaryDto = _mapper.Map<List<VehicleSummaryDto>>(summaries);
                return new ResponseDto { IsSuccess = true, Result = vehicleSummaryDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<ResponseDto> GetVehicleSummaryByVehicleNoAsync(string vehicleNo)
        {
            try
            {
                var vehicleSummary = await _vehicleSummaryRepository.GetVehicleSummaryByVehicleNoAsync(vehicleNo);
                if (vehicleSummary == null)
                {
                    return new ResponseDto { IsSuccess = false, Message = "No vehicle summary found for the specified vehicle number." };
                }

                var vehicleSummaryDto = _mapper.Map<VehicleSummaryDto>(vehicleSummary);
                return new ResponseDto { IsSuccess = true, Result = vehicleSummaryDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<VehicleSummaryDto> GetVehicleSummaryAsync(int vehicleId, DateTime month)
        {
            var vehicleSummary = await _vehicleSummaryRepository.GetVehicleSummary(vehicleId, month);

            vehicleSummary = vehicleSummary ?? throw new KeyNotFoundException($"No vehicle summary found for vehicle ID {vehicleId} and month {month}.");

            var vehicleSummaryDto = _mapper.Map<VehicleSummaryDto>(vehicleSummary);

            return vehicleSummaryDto;
        }


        public async Task<VehicleSummaryDto> UpdateSummaryAsync(int vehicleId, int locationId, DateTime month, IEnumerable<TripLog> tripLogs)
        {
            return await _vehicleSummaryFactory.CreateOrUpdateVehicleSummaryAsync(vehicleId, locationId, month, tripLogs);
        }


        public async Task UpdateVehicleSummariesFromTripLogsAsync()
        {
            // Retrieve all trip logs
            var allTripLogs = await _triplogRepository.GetAllTripLogsAsync();

            if (allTripLogs == null || !allTripLogs.Any())
            {
                throw new ArgumentException("No trip logs available for processing.", nameof(allTripLogs));
            }

            // Group trip logs by vehicleId and month
            var groupedTripLogs = allTripLogs
                .GroupBy(tl => new { tl.VehicleId, Month = new DateTime(tl.Date.Year, tl.Date.Month, 1) });

            foreach (var group in groupedTripLogs)
            {
                var vehicleId = group.Key.VehicleId;
                var month = group.Key.Month;
                var tripLogs = group.ToList();

                // Calculate summary values for the grouped trip logs
                var summaryDto = await CalculateVehicleSummaryAsync(vehicleId, month, tripLogs);

                // You can log or handle the summaryDto as needed
            }
        }

        public async Task<VehicleSummaryDto> CalculateVehicleSummaryAsync(int vehicleId, DateTime month, IEnumerable<TripLog> tripLogs)
        {
            // Check if tripLogs is empty
            if (tripLogs == null || !tripLogs.Any())
            {
                throw new ArgumentException("Trip logs cannot be null or empty.", nameof(tripLogs));
            }

            // Calculate summary values
            var firstTripLog = tripLogs.OrderBy(tl => tl.Date).First();
            var lastTripLog = tripLogs.OrderByDescending(tl => tl.Date).First();

            var totalDaysFilledDiesel = tripLogs.Count(tl => tl.DieselLiters > 0);
            var totalDiesel = tripLogs.Sum(tl => tl.DieselLiters);
            var openingKms = firstTripLog.StartingKm;
            var closingKms = lastTripLog.StartingKm;
            var totalKmRun = closingKms - openingKms;
            var average = totalDiesel > 0 ? (decimal)totalKmRun / totalDiesel : 0;

            // Check if the summary already exists
            var existingSummary = await _vehicleSummaryRepository.GetVehicleSummary(vehicleId, month);

            VehicleSummary summary;

            if (existingSummary != null)
            {
                // Update existing summary
                existingSummary.UpdateSummary(totalDaysFilledDiesel, totalDiesel, openingKms, closingKms, totalKmRun, average);
                summary = existingSummary;
                await _vehicleSummaryRepository.UpdateVehicleSummary(existingSummary);
            }
            else
            {
                // Create new summary
                // Use a default location ID or another logic to set this if existingSummary is null
                int locationId = firstTripLog.LocationId; // Assuming you want to use the location from the first trip log
                summary = new VehicleSummary(vehicleId, locationId, month, totalDaysFilledDiesel, totalDiesel, openingKms, closingKms, totalKmRun, average);
                await _vehicleSummaryRepository.AddVehicleSummary(summary);
            }

            return new VehicleSummaryDto
            {
                SummaryId = summary.SummaryId,
                SRNo = summary.SRNo,
                Month = summary.Month,
                VehicleNo = firstTripLog.Vehicle.VehicleNo,
                LocationId = summary.LocationId,
                TotalDaysFilledDiesel = summary.TotalDaysFilledDiesel,
                TotalDiesel = summary.TotalDiesel,
                OpeningKms = summary.OpeningKms,
                ClosingKms = summary.ClosingKms,
                TotalKmRun = summary.TotalKmRun,
                Average = summary.Average
            };
        }


    }


}
