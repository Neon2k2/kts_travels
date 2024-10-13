using kts_travels.Application.Dtos;
using kts_travels.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace kts_travels.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripLogsController(ITripLogService service, ILogger<TripLogsController> logger) : ControllerBase
    {
        private readonly ITripLogService _service = service;
        private readonly ILogger<TripLogsController> _logger = logger;


        // POST api/triplogs/add
        [HttpPost("add")]
        public async Task<IActionResult> Create([FromBody] TripLogDto log)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _service.AddTripLogAsync(log);
            if (response.IsSuccess)
            {
                return CreatedAtAction(nameof(GetById), new { id = log.TripId }, response); // Ensure you return the proper status code and location
            }
            return BadRequest(response);
        }

        // DELETE api/triplogs/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteTripLogAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response); // Use NotFound for non-existent trip logs
        }

        // GET api/triplogs/logs
        [HttpGet("logs")]
        public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _service.GetAllTripLogsAsync(pageNumber, pageSize);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // GET api/triplogs/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetTripLogByIdAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response); // Return 404 if not found
        }

        // PUT api/triplogs/update
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] TripLogDto log)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _service.UpdateTripLogAsync(log);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // GET api/triplogs/search/daterange
        [HttpGet("search/daterange")]
        public async Task<IActionResult> SearchByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var response = await _service.SearchTripLogsByDateRangeAsync(startDate, endDate);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // GET api/triplogs/search/vehicle
        [HttpGet("search/vehicle")]
        public async Task<IActionResult> SearchByVehicleNo([FromQuery] string vehicleNo)
        {
            var response = await _service.SearchTripLogsByVehicleNoAsync(vehicleNo);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}

