﻿using kts_travels.Core.Application.Dtos;
using kts_travels.Core.Domain.Entities;

namespace kts_travels.Core.Application.Services.Interfaces
{
    public interface IVehicleSummariesService
    {
        Task<ResponseDto> GetVehicleSummariesAsync();
        Task<ResponseDto> GetVehicleSummaryByVehicleNoAsync(string vehicleNo);
        Task<VehicleSummaryDto> GetVehicleSummaryAsync(int vehicleId, DateTime month);
        Task<VehicleSummaryDto> UpdateSummaryAsync(int vehicleId, int locationId, DateTime month, IEnumerable<TripLog> tripLogs);
        Task UpdateVehicleSummariesFromTripLogsAsync();
        Task<VehicleSummaryDto> CalculateVehicleSummaryAsync(int vehicleId, DateTime month, IEnumerable<TripLog> tripLogs);
    }

}
