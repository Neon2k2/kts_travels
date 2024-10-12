using kts_travels.SharedServices.Application.Dtos;
using kts_travels.SharedServices.Application.Services;
using kts_travels.SharedServices.Domain.Entities;
using kts_travels.SharedServices.Domain.Repositories;

namespace kts_travels.SharedServices.Application.Factories.Interfaces
{
    public interface IVehicleSummariesFactory
    {
        Task<VehicleSummaryDto> CreateOrUpdateVehicleSummaryAsync(int vehicleId, int locationId, DateTime month, IEnumerable<TripLog> tripLogs);
    }
}
