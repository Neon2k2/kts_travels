using kts_travels.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace kts_travels.WebAPI.Controllers
{
    public class VehicleSummariesController(IVehicleSummariesService service): ControllerBase
    {
        private readonly IVehicleSummariesService _service = service;


        [HttpGet("summaries")] // Route to get all vehicle summaries
        public async Task<IActionResult> GetVehicleSummaries()
        {
            var response = await _service.GetVehicleSummariesAsync(); // Call the service method
            if (response.IsSuccess)
            {
                return Ok(response); // Return a 200 OK response with the result
            }
            return StatusCode(500, response); // Return a 500 Internal Server Error response if unsuccessful
        }


        [HttpGet("summary/vehicle/{vehicleNo}")]
        public async Task<IActionResult> GetSummaryByVehicleNo(string vehicleNo)
        {
            var response = await _service.GetVehicleSummaryByVehicleNoAsync(vehicleNo);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

    }

}
