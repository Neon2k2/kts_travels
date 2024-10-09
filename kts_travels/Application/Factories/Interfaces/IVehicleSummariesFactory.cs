using kts_travels.Application.Dtos;
using kts_travels.Application.Services;
using kts_travels.Domain.Entities;
using kts_travels.Domain.Repositories;

namespace kts_travels.Application.Factories.Interfaces
{
    public interface IVehicleSummariesFactory
    {
        Task<VehicleSummaryDto> CreateOrUpdateVehicleSummaryAsync(int vehicleId, int locationId, DateTime month, IEnumerable<TripLog> tripLogs);
    }
}
