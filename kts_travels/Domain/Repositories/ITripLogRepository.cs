using kts_travels.SharedServices.Domain.Entities;

namespace kts_travels.SharedServices.Domain.Repositories
{
    public interface ITripLogRepository
    {
        Task<TripLog> GetTripLogByIdAsync(int id);
        Task<TripLog> GetLatestTripLogBeforeDateAsync(string vehicleNo, DateTime date);
        Task<TripLog> GetTripLogByStartingKmAsync(string vehicleNo, int startingKm);
        Task<IEnumerable<TripLog>> GetAllTripLogsAsync(int pageNumber, int pageSize);
        Task<IEnumerable<TripLog>> GetAllTripLogsAsync();
        Task<bool> AddTripLogAsync(TripLog tripLog);
        Task<bool> UpdateTripLogAsync(TripLog tripLog);
        Task<bool> DeleteTripLogAsync(int id);
        Task<IEnumerable<TripLog>> SearchTripLogsByVehicleNoAsync(string vehicleNo);
        Task<IEnumerable<TripLog>> SearchTripLogsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TripLog>> GetTripLogsForVehicleAndMonthAsync(string vehicleNo, DateTime month);
    }
}
