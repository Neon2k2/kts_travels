using kts_travels.Models.Dto;

namespace kts_travels.Services.IServices
{
    public interface IDieselEntryService
    {
        Task<ResponseDto> GetAllEntriesAsync();

        Task<ResponseDto> GetEntriesByLocationAndVehicleNoAsync(string location, string vehicleNo);

        Task<ResponseDto> GetAveragesByMonthAsync(string month, string year, string location = null, string vehicleNo = null);

        Task<ResponseDto> AddDieselEntryAsync(DieselEntryDto entryDto);
    }


}
