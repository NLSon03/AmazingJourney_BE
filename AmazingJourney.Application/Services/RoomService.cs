using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazingJourney.Application.DTOs;
using AmazingJourney.Application.Interfaces;
using AmazingJourney.Infrastructure.Data;
using AmazingJourney_BE.AmazingJourney.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AmazingJourney.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RoomService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Lấy tất cả các phòng kèm theo hình ảnh
        public async Task<IEnumerable<RoomDTO>> GetAllRoomsAsync()
        {
            var rooms = await _context.Rooms
                .Include(r => r.RoomImages) // Tải kèm các ảnh
                .ToListAsync();

            return _mapper.Map<IEnumerable<RoomDTO>>(rooms);
        }

        // Lấy phòng cụ thể theo ID kèm hình ảnh
        public async Task<RoomDTO> GetRoomWithImagesByIdAsync(int roomId)
        {
            var room = await _context.Rooms
                .Include(r => r.RoomImages)
                .Where(r => r.Id == roomId)
                .FirstOrDefaultAsync();

            return _mapper.Map<RoomDTO>(room);
        }

        // Lấy danh sách phòng theo HotelId
        public async Task<IEnumerable<RoomDTO>> GetRoomsByHotelIdAsync(int hotelId)
        {
            var rooms = await _context.Rooms
                .Include(r => r.RoomImages)  // Bao gồm hình ảnh
                .Where(r => r.HotelId == hotelId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<RoomDTO>>(rooms);
        }

        // Tạo phòng mới
        public async Task<RoomDTO> CreateRoomAsync(RoomDTO roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            // Thiết lập thời gian tạo và cập nhật ban đầu
            room.CreatedAt = DateTime.UtcNow;
            room.UpdatedAt = DateTime.UtcNow;
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return _mapper.Map<RoomDTO>(room);
        }

        // Cập nhật phòng
        public async Task<RoomDTO> UpdateRoomAsync(int id, RoomDTO roomDto)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return null;

            _mapper.Map(roomDto, room);
            // Cập nhật thời gian khi có thay đổi
            room.UpdatedAt = DateTime.UtcNow;
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();

            return _mapper.Map<RoomDTO>(room);
        }

        // Xóa phòng
        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return false;

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return true;
        }

        // Lấy phòng cụ thể theo ID
        public async Task<RoomDTO> GetRoomByIdAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            return room == null ? null : _mapper.Map<RoomDTO>(room);
        }

        // Cài đặt phương thức thêm ảnh cho phòng
        public async Task AddRoomImageAsync(RoomImageDTO roomImageDto)
        {
            var roomImage = _mapper.Map<RoomImage>(roomImageDto);
            _context.RoomImages.Add(roomImage);
            await _context.SaveChangesAsync();
        }

        public async Task<string> SaveRoomImageAsync(IFormFile file)
        {
            if (file.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "roomImages");

                // Kiểm tra nếu thư mục không tồn tại thì tạo mới
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return "/roomImages/" + uniqueFileName;
            }

            return null;
        }

    }

}
