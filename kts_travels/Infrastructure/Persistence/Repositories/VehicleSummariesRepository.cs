using kts_travels.SharedServices.Domain.Entities;
using kts_travels.SharedServices.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace kts_travels.SharedServices.Infrastructure.Persistence.Repositories
{
public class VehicleSummariesRepository(AppDbContext context) : IVehicleSummariesRepository
{
    private readonly AppDbContext _context = context;

    // Method to retrieve all vehicle summaries
    public async Task<IEnumerable<VehicleSummary>> GetSummariesAsync()
    {
        return await _context.VehicleSummaries.ToListAsync();
    }

    // Method to get a specific vehicle summary by vehicle ID and month
    public async Task<VehicleSummary> GetVehicleSummary(int vehicleId, DateTime month)
    {
        return await _context.VehicleSummaries
            .FirstOrDefaultAsync(vs => vs.VehicleId == vehicleId && vs.Month.Month == month.Month && vs.Month.Year == month.Year);
    }

    public async Task<VehicleSummary> GetVehicleSummaryByVehicleNoAsync(string vehicleNo)
    {
        return await _context.VehicleSummaries
            .FirstOrDefaultAsync(vs => vs.Vehicle.VehicleNo == vehicleNo);
    }

    // Method to update an existing vehicle summary
    public async Task UpdateVehicleSummary(VehicleSummary summary)
    {
        _context.VehicleSummaries.Update(summary); // Use VehicleSummaries instead of Set<VehicleSummary>()
        await _context.SaveChangesAsync();
    }

    // Method to add a new vehicle summary
    public async Task AddVehicleSummary(VehicleSummary summary)
    {
        await _context.VehicleSummaries.AddAsync(summary); // Use VehicleSummaries instead of Set<VehicleSummary>()
        await _context.SaveChangesAsync();
    }
}


}
