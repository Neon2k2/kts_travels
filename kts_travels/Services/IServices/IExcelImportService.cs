using kts_travels.Models.Dto;

namespace kts_travels.Services.IServices
{
    public interface IExcelImportService
    {
        Task<ResponseDto> ImportDataAsync(IFormFile file);
    }
}
