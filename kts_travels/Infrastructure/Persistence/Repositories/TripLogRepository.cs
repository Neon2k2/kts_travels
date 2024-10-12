using kts_travels.SharedServices.Domain.Entities;
using kts_travels.SharedServices.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace kts_travels.SharedServices.Infrastructure.Persistence.Repositories
{
    public class TripLogRepository(AppDbContext context, ILogger<TripLogRepository> logger) : ITripLogRepository
    {
        private readonly AppDbContext _context = context;
        private readonly ILogger<TripLogRepository> _logger = logger;


        public async Task<bool> AddTripLogAsync(TripLog tripLog)
        {
            await _context.TripLogs.AddAsync(tripLog);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }


        public async Task<TripLog> GetLatestTripLogBeforeDateAsync(string vehicleNo, DateTime date)
        {
            return await _context.TripLogs
                .Where(t => t.Vehicle.VehicleNo == vehicleNo && t.Date < date)
                .OrderByDescending(t => t.Date)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TripLog>> GetTripLogsForVehicleAndMonthAsync(string vehicleNo, DateTime month)
        {
            var startDate = new DateTime(month.Year, month.Month, 1);
            var endDate = startDate.AddMonths(1);

            return await _context.TripLogs
                .Where(tl => tl.Vehicle.VehicleNo == vehicleNo && tl.Date >= startDate && tl.Date < endDate)
                .ToListAsync();
        }

        public async Task<TripLog> GetTripLogByStartingKmAsync(string vehicleNo, int startingKm)
        {
            return await _context.TripLogs
                .Where(t => t.Vehicle.VehicleNo == vehicleNo && t.StartingKm == startingKm)
                .FirstOrDefaultAsync();
        }


        public async Task<bool> DeleteTripLogAsync(int id)
        {
            var tripLog = await _context.TripLogs.FindAsync(id);
            if (tripLog != null)
            {
                _context.TripLogs.Remove(tripLog);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }


        public async Task<IEnumerable<TripLog>> GetAllTripLogsAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 1 : pageSize;
            var tripLogs = await _context.TripLogs
                .Include(t => t.Vehicle)
                .Include(t => t.Location)
                .OrderByDescending(t => t.TripId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return tripLogs;
        }
        public async Task<IEnumerable<TripLog>> GetAllTripLogsAsync()
        {
            var triplogs = await _context.TripLogs.ToListAsync();
            return triplogs;
        }

        public async Task<TripLog> GetTripLogByIdAsync(int id)
        {
            var tripLog = await _context.TripLogs
                .Include(t => t.Vehicle)
                .Include(t => t.Location)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TripId == id);

            if (tripLog == null)
            {
                _logger.LogWarning("TripLog with ID {TripId} not found.", id);
            }

            return tripLog;
        }

        public async Task<IEnumerable<TripLog>> SearchTripLogsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.TripLogs
                .Include(t => t.Vehicle)
                .Include(t => t.Location)
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .OrderByDescending(t => t.TripId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TripLog>> SearchTripLogsByVehicleNoAsync(string vehicleNo)
        {
            return await _context.TripLogs
                .Include(t => t.Vehicle)
                .Include(t => t.Location)
                .Where(t => t.Vehicle.VehicleNo.Contains(vehicleNo))
                .OrderByDescending(t => t.TripId)
                .ToListAsync();
        }

        public async Task<bool> UpdateTripLogAsync(TripLog tripLog)
        {
            _context.TripLogs.Update(tripLog);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }


    }
}
