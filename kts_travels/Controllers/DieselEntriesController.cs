using AutoMapper;
using kts_travels.Data;
using kts_travels.Models.Dto;
using kts_travels.Services;
using kts_travels.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kts_travels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DieselEntriesController : ControllerBase
    {
        private readonly IDieselEntryService _dieselEntryService;
        private readonly ILogger<DieselEntriesController> _logger;
        private readonly IExcelImportService _excelImportService;

        public DieselEntriesController(IDieselEntryService dieselEntryService, ILogger<DieselEntriesController> logger, IExcelImportService excelImportService)
        {
            _dieselEntryService = dieselEntryService;
            _logger = logger;
            _excelImportService = excelImportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _dieselEntryService.GetAllEntriesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all diesel entries.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{location}/{vehicleNo}")]
        public async Task<IActionResult> GetByLocationAndVehicleNo(string location, string vehicleNo)
        {
            try
            {
                var response = await _dieselEntryService.GetEntriesByLocationAndVehicleNoAsync(location, vehicleNo);
                if (!response.IsSuccess)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching diesel entries for location: {location} and vehicleNo: {vehicleNo}", location, vehicleNo);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("averages/{year}/{month}/{location?}/{vehicleNo?}")]
        public async Task<IActionResult> GetAveragesByMonth(string month, string year, string location = null, string vehicleNo = null)
        {
            try
            {
                var response = await _dieselEntryService.GetAveragesByMonthAsync(month, year, location, vehicleNo);
                if (!response.IsSuccess)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while calculating averages for month: {month}, year: {year}, location: {location}, vehicleNo: {vehicleNo}", month, year, location, vehicleNo);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDieselEntry([FromBody] DieselEntryDto entryDto)
        {
            try
            {
                if (entryDto == null)
                    return BadRequest("Diesel entry data is null");

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); // Return validation errors
                }

                var response = await _dieselEntryService.AddDieselEntryAsync(entryDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding diesel entry.");
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPost("import")]
        public async Task<IActionResult> ImportData([FromForm] IFormFile file)
        {
            try
            {
                if (file == null)
                    return BadRequest("File is required.");

                var response = await _excelImportService.ImportDataAsync(file);
                if (!response.IsSuccess)
                    return BadRequest(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while importing data from Excel.");
                return StatusCode(500, "Internal server error occurred.");
            }
        }
    }
}
