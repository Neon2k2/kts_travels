using kts_travels.Application.Dtos;

namespace kts_travels.Application.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<ResponseDto> AddVehicleAsync(VehicleDto vehicleDto);
        Task<ResponseDto> GetVehiclesAsync(int pageNumber, int pageSize);
        Task<ResponseDto> UpdateVehicleAsync(VehicleDto vehicleDto);
        Task<ResponseDto> DeleteVehicleAsync(int id);
        Task<ResponseDto> GetVehicleAsync(int id);
        Task<ResponseDto> GetVehicleByNoAsync(string vehicleNo);
    }
}
