using AmazingJourney.Application.DTOs;
using AmazingJourney.Application.Interfaces;

using AmazingJourney.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AmazingJourney.Application.Services;

namespace AmazingJourney_BE.AmazingJourney.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HotelImageController : ControllerBase
    {
        private readonly IHotelImageService _hotelImageService;
        private readonly IHotelService _hotelService;
        public HotelImageController(IHotelImageService hotelImageService, IHotelService hotelService)
        {
            _hotelImageService = hotelImageService;
            _hotelService = hotelService;
        }

        // GET: api/HotelImage/GetImagesByHotelId/5
        [HttpGet("GetImagesByHotelId/{hotelId}")]
        public async Task<IActionResult> GetImagesByHotelId(int hotelId)
        {
            var images = await _hotelImageService.GetImagesByHotelIdAsync(hotelId);
            return Ok(images);
        }
        [HttpPost("UploadFile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromForm] int hotelId)
        {
            // Kiểm tra xem file có hợp lệ không
            if (file == null || file.Length == 0)
                return BadRequest("File is not selected");

            // Kiểm tra xem hotelId có tồn tại trong hệ thống không
            var hotel = await _hotelService.GetHotelByIdAsync(hotelId);
            if (hotel == null)
            {
                return BadRequest("Hotel ID is invalid or does not exist.");
            }

            // Lưu file vào hệ thống
            var filePath = await _hotelImageService.SaveImageAsync(file);
            if (string.IsNullOrEmpty(filePath))
                return StatusCode(500, "An error occurred while saving the image.");

            // Tạo đối tượng HotelImageDTO với hotelId hợp lệ
            var hotelImageDto = new HotelImageDTO
            {
                HotelId = hotelId,  // Thêm hotelId
                ImageUrl = filePath
            };

            // Gọi service để upload ảnh
            var image = await _hotelImageService.UploadImageAsync(hotelImageDto);

            // Trả về CreatedAtAction với thông tin hình ảnh mới được upload
            return CreatedAtAction(nameof(GetImagesByHotelId), new { hotelId = image.HotelId }, image);
        }

        // DELETE: api/HotelImage/Delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var success = await _hotelImageService.DeleteImageAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
