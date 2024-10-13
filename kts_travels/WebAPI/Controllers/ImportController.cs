using kts_travels.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kts_travels.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly IExcelImportService _excelImportService;
        private readonly IVehicleSummariesService _service;

        public ImportController(IExcelImportService excelImportService, IVehicleSummariesService service)
        {
            _excelImportService = excelImportService;
            _service = service;
        }

        [HttpPost("import-excel")]
        public async Task<IActionResult> ImportVehiclesFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty or not provided.");
            }

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    await _excelImportService.ImportDataFromExcelAsync(stream);
                }
                await _service.UpdateVehicleSummariesFromTripLogsAsync();
                return Ok("Data imported successfully.");
            }
            catch (Exception ex)
            {
                // Log the error and return a generic error response
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
