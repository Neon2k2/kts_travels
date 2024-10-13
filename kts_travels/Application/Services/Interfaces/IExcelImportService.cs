namespace kts_travels.Application.Services.Interfaces
{
    public interface IExcelImportService
    {
        Task ImportDataFromExcelAsync(Stream excelFile);
    }
}
