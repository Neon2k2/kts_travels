
using kts_travels.Core.Application.Services;
using kts_travels.Core.Application.Dtos;
using kts_travels.Core.Domain.Entities;
using kts_travels.Core.Domain.Repositories;

namespace kts_travels.Core.Application.Factories.Interfaces
{
    public interface IVehicleSummariesFactory
    {
        Task<VehicleSummaryDto> CreateOrUpdateVehicleSummaryAsync(int vehicleId, int locationId, DateTime month, IEnumerable<TripLog> tripLogs);
    }
}
