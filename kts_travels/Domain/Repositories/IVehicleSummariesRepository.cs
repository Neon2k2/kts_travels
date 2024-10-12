using kts_travels.SharedServices.Application.Dtos;
using kts_travels.SharedServices.Domain.Entities;

namespace kts_travels.SharedServices.Domain.Repositories
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
