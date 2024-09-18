using AmazingJourney.Application.DTOs;
using AmazingJourney.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazingJourney.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // Lấy tất cả các phòng kèm hình ảnh
        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }

        // Lấy phòng cụ thể theo ID kèm hình ảnh
        [HttpGet("with-images/{id}")]
        public async Task<IActionResult> GetRoomWithImages(int id)
        {
            var room = await _roomService.GetRoomWithImagesByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        // Lấy danh sách phòng theo HotelId
        [HttpGet("by-hotel/{hotelId}")]
        public async Task<IActionResult> GetRoomsByHotelId(int hotelId)
        {
            var rooms = await _roomService.GetRoomsByHotelIdAsync(hotelId);
            return Ok(rooms);
        }

        [HttpPost("CreateRoom")]
        public async Task<IActionResult> CreateRoom([FromBody] RoomDTO roomDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdRoom = await _roomService.CreateRoomAsync(roomDto);
            return Ok(createdRoom);
        }


        [HttpPost("UploadRoomImage/{roomId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadRoomImage([FromRoute] int roomId, [FromForm] IFormFile file)
        {
            // Kiểm tra nếu Room có tồn tại trong database
            var room = await _roomService.GetRoomByIdAsync(roomId);
            if (room == null)
            {
                return NotFound("Room not found.");
            }

            // Lưu ảnh vào server và lấy đường dẫn URL
            var imageUrl = await _roomService.SaveRoomImageAsync(file);
            if (imageUrl == null)
            {
                return BadRequest("Image upload failed.");
            }

            // Tạo RoomImageDTO và thêm ảnh cho Room
            var roomImageDto = new RoomImageDTO
            {
                RoomId = roomId,
                ImageUrl = imageUrl
            };

            await _roomService.AddRoomImageAsync(roomImageDto);

            // Trả về RoomImageDTO với thông tin của ảnh đã được upload
            return Ok(roomImageDto);
        }


        // Cập nhật phòng
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomDTO roomDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedRoom = await _roomService.UpdateRoomAsync(id, roomDto);
            if (updatedRoom == null)
                return NotFound();

            return Ok(updatedRoom);
        }

        // Xóa phòng
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var success = await _roomService.DeleteRoomAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
