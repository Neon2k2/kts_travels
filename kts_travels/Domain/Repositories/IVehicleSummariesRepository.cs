using kts_travels.Application.Dtos;
using kts_travels.Domain.Entities;

namespace kts_travels.Domain.Repositories
{
    public interface IVehicleSummariesRepository
    {
        Task<IEnumerable<VehicleSummary>> GetSummariesAsync();
        Task<VehicleSummary> GetVehicleSummary(int vehicleId, DateTime month);
        Task UpdateVehicleSummary(VehicleSummary summary);
        Task AddVehicleSummary(VehicleSummary summary);
        Task<VehicleSummary> GetVehicleSummaryByVehicleNoAsync(string vehicleNo);
    }
}
