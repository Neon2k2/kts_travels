namespace kts_travels.Core.Application.Services.Interfaces
{
    public interface IExcelImportService
    {
        Task ImportDataFromExcelAsync(Stream excelFile);
    }
}
