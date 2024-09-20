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
    public class HotelService : IHotelService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HotelService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HotelDTO>> GetAllHotelsAsync()
        {
            var hotels = await _context.Hotels.Include(h => h.HotelImages).Include(h => h.Rooms).ThenInclude(r=>r.RoomImages).ToListAsync();

            if (hotels == null)
            {
                return null;
            }
            return _mapper.Map<IEnumerable<HotelDTO>>(hotels);
        }

        // Sử dụng Include để tải kèm dữ liệu bảng liên quan
        public async Task<HotelDTO> GetHotelWithImagesByIdAsync(int hotelId)
        {
            var hotel = await _context.Hotels
            .Include(h => h.HotelImages) // Bao gồm hình ảnh của khách sạn
            .Include(h => h.Rooms)       // Bao gồm danh sách phòng
            .ThenInclude(r => r.RoomImages) // Bao gồm hình ảnh của từng phòng
            .FirstOrDefaultAsync(h => h.Id == hotelId);

            return _mapper.Map<HotelDTO>(hotel);
        }

        // Lấy khách sạn không bao gồm hình ảnh (nếu cần)
        public async Task<HotelDTO> GetHotelByIdAsync(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            return hotel == null ? null : _mapper.Map<HotelDTO>(hotel);
        }

        public async Task<HotelDTO> CreateHotelAsync(HotelDTO hotelDto)
        {
            var hotel = _mapper.Map<Hotel>(hotelDto);
            hotel.CreatedAt = DateTime.UtcNow;
            hotel.UpdatedAt = DateTime.UtcNow;
            // Kiểm tra nếu không có Rooms thì không tự động tạo Rooms trống
            if (hotelDto.Rooms == null || !hotelDto.Rooms.Any())
            {
                hotel.Rooms = new List<Room>();  // Không để Room tự động được tạo
            }
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return _mapper.Map<HotelDTO>(hotel);
        }


       // Cập nhật khách sạn
      public async Task<HotelDTO> UpdateHotelAsync(int id, HotelDTO hotelDto)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
                return null;

            _mapper.Map(hotelDto, hotel);
            hotel.UpdatedAt = DateTime.UtcNow;
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();

            return _mapper.Map<HotelDTO>(hotel);
        }

        // Xóa khách sạn
        public async Task<bool> DeleteHotelAsync(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
                return false;

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return true;
        }

        // Tìm kiếm khách sạn theo tên
        public async Task<IEnumerable<HotelDTO>> GetHotelByNameAsync(string name)
        {
            var hotels = await _context.Hotels
                .Include(h => h.HotelImages)  // Bao gồm hình ảnh
                .Where(h => h.Name.Contains(name))
                .ToListAsync();

            return _mapper.Map<IEnumerable<HotelDTO>>(hotels);
        }

        // Lấy danh sách khách sạn theo Category
        public async Task<IEnumerable<HotelDTO>> GetListHotelByCategoryId(int categoryId)
        {
            var hotels = await _context.Hotels
                .Include(h => h.HotelImages)  // Bao gồm hình ảnh
                .Where(h => h.CategoryId == categoryId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<HotelDTO>>(hotels);
        }

        // Lấy danh sách khách sạn theo Location
        public async Task<IEnumerable<HotelDTO>> GetListHotelByLocationId(int locationId)
        {
            var hotels = await _context.Hotels
                .Include(h => h.HotelImages)  // Bao gồm hình ảnh
                .Where(h => h.LocationId == locationId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<HotelDTO>>(hotels);
        }

        public async Task AddHotelImageAsync(HotelImageDTO hotelImageDTO)
        {
            var hotelImage = _mapper.Map<HotelImage>(hotelImageDTO);
            _context.HotelImages.Add(hotelImage);
            await _context.SaveChangesAsync();
        }
        public async Task<string> SaveImageAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "hotelImages");

            // Kiểm tra nếu thư mục upload không tồn tại, thì tạo mới
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Tạo tên file duy nhất để tránh trùng lặp
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Lưu file vào đường dẫn
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Trả về đường dẫn tương đối của file để lưu vào database
            return Path.Combine("hotels", uniqueFileName).Replace("\\", "/");
        }

        //Task<HotelDTO> IHotelService.GetHotelWithImagesByIdAsync(int hotelId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
