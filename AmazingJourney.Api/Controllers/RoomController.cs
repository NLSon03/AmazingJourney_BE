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
        [HttpGet("{id}/with-images")]
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

        // Tạo phòng mới
        /* [HttpPost]
         public async Task<IActionResult> CreateRoom([FromBody] RoomDTO roomDto)
         {
             if (!ModelState.IsValid)
                 return BadRequest(ModelState);

             var room = await _roomService.CreateRoomAsync(roomDto);
             return CreatedAtAction(nameof(GetRoomWithImages), new { id = room.Id }, room);
         }
        */

        [HttpPost("UploadFile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateRoomWithImage([FromForm] RoomDTO roomDto, [FromForm] IFormFile file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Lưu ảnh nếu có file upload
            string filePath = null;
            if (file != null)
            {
                filePath = await _roomService.SaveRoomImageAsync(file);
            }

            // Tạo phòng
            var room = await _roomService.CreateRoomAsync(roomDto);

            // Lưu thông tin RoomImage nếu có file ảnh
            if (!string.IsNullOrEmpty(filePath))
            {
                var roomImage = new RoomImageDTO
                {
                    RoomId = room.Id,
                    ImageUrl = filePath
                };
                await _roomService.AddRoomImageAsync(roomImage);
            }

            return CreatedAtAction(nameof(GetRoomWithImages), new { id = room.Id }, room);
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
