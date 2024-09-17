using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazingJourney.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace AmazingJourney.Application.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDTO>> GetAllRoomsAsync();
        Task<RoomDTO> GetRoomByIdAsync(int id);

        Task<RoomDTO> GetRoomWithImagesByIdAsync(int roomId);
        Task<IEnumerable<RoomDTO>> GetRoomsByHotelIdAsync(int hotelId);
        Task<RoomDTO> CreateRoomAsync(RoomDTO roomDto);
        Task<string> SaveRoomImageAsync(IFormFile file);

        // Phương thức thêm ảnh cho phòng
        Task AddRoomImageAsync(RoomImageDTO roomImageDto);
        Task<RoomDTO> UpdateRoomAsync(int id, RoomDTO roomDto);
        Task<bool> DeleteRoomAsync(int id);
    }
}
