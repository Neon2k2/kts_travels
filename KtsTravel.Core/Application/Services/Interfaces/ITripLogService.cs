using kts_travels.Core.Application.Dtos;

namespace kts_travels.Core.Application.Services.Interfaces
{
    public interface ITripLogService
    {
        Task<ResponseDto> AddTripLogAsync(TripLogDto triplog);
        Task<ResponseDto> GetTripLogByIdAsync(int id);
        Task<ResponseDto> GetAllTripLogsAsync(int pageNumber, int pageSize);
        Task<ResponseDto> UpdateTripLogAsync(TripLogDto triplog);
        Task<ResponseDto> DeleteTripLogAsync(int id);
        Task<ResponseDto> SearchTripLogsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<ResponseDto> SearchTripLogsByVehicleNoAsync(string VehicleNo);
    }
}
