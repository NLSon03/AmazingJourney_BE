using AmazingJourney.Application.DTOs;
using AmazingJourney.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazingJourney.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        // GET: api/Location
        [HttpGet("GetAllLocations")]
        public async Task<IActionResult> GetAllLocations()
        {
            var locations = await _locationService.GetAllLocationsAsync();
            return Ok(locations);
        }

        // GET: api/Location/5
        [HttpGet("GetLocationById/{id}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            if (location == null)
                return NotFound();

            return Ok(location);
        }

        // POST: api/Location
        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] LocationDTO locationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var location = await _locationService.CreateLocationAsync(locationDto);
            return CreatedAtAction(nameof(GetLocationById), new { id = location.Id }, location);
        }

        // PUT: api/Location/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] LocationDTO locationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedLocation = await _locationService.UpdateLocationAsync(id, locationDto);
            if (updatedLocation == null)
                return NotFound();

            return Ok(updatedLocation);
        }

        // DELETE: api/Location/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var success = await _locationService.DeleteLocationAsync(id);
            if (!success)
                return NotFound();

            return Ok($"Deleted successfully LocationId: {id}");
        }
    }

}
