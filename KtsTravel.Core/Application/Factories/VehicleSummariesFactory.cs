using kts_travels.Core.Application.Dtos;
using kts_travels.Core.Application.Factories.Interfaces;
using kts_travels.Core.Application.Dtos;
using kts_travels.Core.Domain.Entities;
using kts_travels.Core.Domain.Repositories;
using kts_travels.Core.Application.Dtos;
using kts_travels.Core.Application.Dtos;

namespace kts_travels.Core.Application.Factories
{
    public class VehicleSummariesFactory(IVehicleSummariesRepository vehicleSummaryRepository) : IVehicleSummariesFactory
    {
        private readonly IVehicleSummariesRepository _vehicleSummaryRepository = vehicleSummaryRepository;

        public async Task<VehicleSummaryDto> CreateOrUpdateVehicleSummaryAsync(int vehicleId, int locationId, DateTime month, IEnumerable<TripLog> tripLogs)
        {
            if (tripLogs == null || !tripLogs.Any())
            {
                throw new ArgumentException("Trip logs cannot be null or empty", nameof(tripLogs));
            }

            var (totalDaysFilledDiesel, totalDiesel, openingKms, closingKms, totalKmRun, average) = CalculateSummaryValues(tripLogs);
            var existingSummary = await _vehicleSummaryRepository.GetVehicleSummary(vehicleId, month);

            if (existingSummary != null)
            {
                existingSummary.UpdateSummary(totalDaysFilledDiesel, totalDiesel, openingKms, closingKms, totalKmRun, average);
                await _vehicleSummaryRepository.UpdateVehicleSummary(existingSummary);
            }
            else
            {
                var newSummary = new VehicleSummary(vehicleId, locationId, month, totalDaysFilledDiesel, totalDiesel, openingKms, closingKms, totalKmRun, average);
                await _vehicleSummaryRepository.AddVehicleSummary(newSummary);
                existingSummary = newSummary; // use newSummary for DTO creation
            }

            return new VehicleSummaryDto
            {
                SummaryId = existingSummary.SummaryId,
                SRNo = existingSummary.SRNo,
                Month = existingSummary.Month,
                VehicleNo = tripLogs.First().Vehicle.VehicleNo, // Use the related vehicle's vehicle number                                                                            // Use the first trip log's vehicle number
                LocationId = existingSummary.LocationId,
                TotalDaysFilledDiesel = existingSummary.TotalDaysFilledDiesel,
                TotalDiesel = existingSummary.TotalDiesel,
                OpeningKms = existingSummary.OpeningKms,
                ClosingKms = existingSummary.ClosingKms,
                TotalKmRun = existingSummary.TotalKmRun,
                Average = existingSummary.Average
            };
        }

        private static (int TotalDaysFilledDiesel, decimal TotalDiesel, int OpeningKms, int ClosingKms, int TotalKmRun, decimal Average) CalculateSummaryValues(IEnumerable<TripLog> tripLogs)
        {
            var firstTripLog = tripLogs.OrderBy(tl => tl.Date).First();
            var lastTripLog = tripLogs.OrderByDescending(tl => tl.Date).First();

            var totalDaysFilledDiesel = tripLogs.Count(tl => tl.DieselLiters > 0);
            var totalDiesel = tripLogs.Sum(tl => tl.DieselLiters);
            var openingKms = firstTripLog.StartingKm;
            var closingKms = lastTripLog.StartingKm;
            var totalKmRun = closingKms - openingKms;
            var average = totalDiesel > 0 ? (decimal)totalKmRun / totalDiesel : 0;

            return (totalDaysFilledDiesel, totalDiesel, openingKms, closingKms, totalKmRun, average);
        }
    }

}
