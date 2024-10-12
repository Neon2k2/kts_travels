namespace kts_travels.SharedServices.Application.Services.Interfaces
{
    public interface IExcelImportService
    {
        Task ImportDataFromExcelAsync(Stream excelFile);
    }
}
