using kts_travels.Application.Dtos;
using kts_travels.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace kts_travels.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController(IVehicleService service, ILogger<VehiclesController> logger) : ControllerBase
    {
        private readonly IVehicleService _service = service;
        private readonly ILogger<VehiclesController> _logger = logger;


        // POST: api/vehicle/add
        [HttpPost("add")]
        public async Task<IActionResult> AddVehicle([FromBody] VehicleDto vehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _service.AddVehicleAsync(vehicleDto);
            if (response.IsSuccess)
            {
                return CreatedAtAction(nameof(GetVehicle), new { vehicleNo = vehicleDto.VehicleNo }, response);
            }

            return BadRequest(response);
        }

        // DELETE: api/vehicle/delete/{vehicleNo}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var response = await _service.DeleteVehicleAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        // GET: api/vehicle/get/{vehicleNo}
        [HttpGet("get/{vehicleNo}")]
        public async Task<IActionResult> GetVehicle([FromRoute] string vehicleNo)
        {
            var response = await _service.GetVehicleByNoAsync(vehicleNo);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        // GET: api/vehicle/viewAll
        [HttpGet("viewAll")]
        public async Task<ActionResult<ResponseDto>> GetAllVehicles(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _service.GetVehiclesAsync(pageNumber, pageSize);
            if (!response.IsSuccess)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // PUT: api/vehicle/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateVehicle([FromBody] VehicleDto vehicleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _service.UpdateVehicleAsync(vehicleDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }

}

