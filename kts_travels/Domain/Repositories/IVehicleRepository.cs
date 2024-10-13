using kts_travels.SharedServices.Domain.Entities;

namespace kts_travels.SharedServices.Domain.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicleByIdAsync(int id);
        Task<Vehicle> GetVehicleByNoAsync(string vehicleNo);
        Task<IEnumerable<Vehicle>> GetAllVehicleAsync(int pageNumber, int pageSize);
        Task<bool> AddVehicleAsync(Vehicle vehicle);
        Task<bool> UpdateVehicleAsync(Vehicle vehicle);
        Task<bool> DeleteVehicleAsync(int id);

    }
}
