using AmazingJourney.Application.DTOs;
using AmazingJourney.Application.Interfaces;
using AmazingJourney_BE.AmazingJourney.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AmazingJourney.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // Lấy danh sách tất cả bookings
        [HttpGet]
        [Authorize] // Chỉ cho phép người dùng đã đăng nhập
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        // Lấy booking theo ID
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound("Booking không tồn tại.");
            }
            return Ok(booking);
        }

        // Tạo mới booking
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDTO bookingCreateDto)
        {
            // Lấy UserId từ Claims của người dùng đã đăng nhập
            int userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            var booking = await _bookingService.CreateBookingAsync(bookingCreateDto, userId);
            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
        }

        // Cập nhật booking
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingDTO bookingDto)
        {
            if (id != bookingDto.Id)
            {
                return BadRequest("ID không khớp.");
            }

            var updatedBooking = await _bookingService.UpdateBookingAsync(id, bookingDto);
            return Ok(updatedBooking);
        }

        // Xóa booking
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var result = await _bookingService.DeleteBookingAsync(id);
            if (!result)
            {
                return NotFound("Booking không tồn tại.");
            }
            return NoContent();
        }
    }
}
