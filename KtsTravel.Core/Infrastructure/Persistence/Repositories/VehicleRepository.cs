using kts_travels.Core.Application.Dtos;
using kts_travels.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using kts_travels.Core.Domain.Repositories;
namespace kts_travels.Core.Infrastructure.Persistence.Repositories
{
    public class VehicleRepository(AppDbContext context, ILogger<VehicleRepository> logger) : IVehicleRepository
    {
        private readonly AppDbContext _context = context;
        private readonly ILogger<VehicleRepository> _logger = logger;


        public async Task<bool> AddVehicleAsync(Vehicle vehicle)
        {
            try
            {
                await _context.Vehicles.AddAsync(vehicle);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while adding a new trip log");
                return false;
            }
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            try
            {
                var vehicle = await _context.Vehicles.FindAsync(id);
                if (vehicle != null)
                {
                    _context.Vehicles.Remove(vehicle);
                    var result = await _context.SaveChangesAsync();
                    return result > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting the vehicle");
                return false;
            }
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehicleAsync(int pageNumber, int pageSize)
        {
            try
            {
                return await _context.Vehicles
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving vehicles.");

                #pragma warning disable IDE0301
                return Enumerable.Empty<Vehicle>();
            }
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            try
            {
                var vehicle = await _context.Vehicles.FindAsync(id);
                if (vehicle == null)
                {
                    _logger.LogWarning("Vehicle with ID {id} not found.", id);
                }
                return vehicle;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving vehicle with ID {id}.", id);
                return null;
            }
        }

        public async Task<Vehicle> GetVehicleByNoAsync(string vehicleNo)
        {
            try
            {
                var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.VehicleNo == vehicleNo);
                if (vehicle == null)
                {
                    _logger.LogWarning("Vehicle with VehicleNo {vehicleNo} not found.", vehicleNo);
                }
                return vehicle;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving vehicle with VehicleNo {vehicleNo}.", vehicleNo);
                return null;
            }
        }


        public async Task<bool> UpdateVehicleAsync(Vehicle vehicle)
        {
            try
            {
                _context.Vehicles.Update(vehicle);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the vehicle");
                return false;
            }
        }
    }
}
