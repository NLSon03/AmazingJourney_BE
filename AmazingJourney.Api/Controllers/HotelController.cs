using AmazingJourney.Application.DTOs;
using AmazingJourney.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazingJourney.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
      

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        // GET: api/Hotel
        [HttpGet("GetAllHotels")]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelService.GetAllHotelsAsync();
            return Ok(hotels);
        }

        // GET: api/Hotel/5
        [HttpGet("GetHotelById/{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelService.GetHotelByIdAsync(id);
            if (hotel == null)
                return NotFound();

            return Ok(hotel);
        }

        // POST: api/Hotel
        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelDTO hotelDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var hotel = await _hotelService.CreateHotelAsync(hotelDto);
                return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, hotel);
            }
            catch (Exception ex)

            {
                // Ghi lại chi tiết lỗi
               
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

      

        // PUT: api/Hotel/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelDTO hotelDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedHotel = await _hotelService.UpdateHotelAsync(id, hotelDto);
            if (updatedHotel == null)
                return NotFound();

            return Ok(updatedHotel);
        }

        // DELETE: api/Hotel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var success = await _hotelService.DeleteHotelAsync(id);
            if (!success)
                return NotFound();

            return Ok("Xóa dữ liệu thành công");
        }

        // GET: api/Hotel/GetHotelByName/{name}
        [HttpGet("GetListHotelByName/{name}")]
        public async Task<IActionResult> GetHotelByName(string name)
        {
            var hotel = await _hotelService.GetHotelByNameAsync(name);
            if (hotel == null)
                return NotFound();

            return Ok(hotel);
        }

        // GET: api/Hotel/GetListHotelByCategoryId/{categoryId
        [HttpGet("GetListHotelByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetListHotelByCategoryId(int categoryId)
        {
            var hotel = await _hotelService.GetListHotelByCategoryId(categoryId);
            if (hotel == null)
                return NotFound();

            return Ok(hotel);
        }

        // GET: api/Hotel/GetListHotelByLocationId/{locationId}
        [HttpGet("GetListHotelByLocationId/{locationId}")]
        public async Task<IActionResult> GetListHotelByLocationId(int locationId)
        {
            var hotel = await _hotelService.GetListHotelByLocationId(locationId);
            if (hotel == null)
                return NotFound();

            return Ok(hotel);
        }
    }
}
