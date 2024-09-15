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
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace AmazingJourney.Application.Services
{
    public class HotelImageService : IHotelImageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HotelImageService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HotelImageDTO>> GetImagesByHotelIdAsync(int hotelId)
        {
            var images = await _context.HotelImages
                                       .Where(img => img.HotelId == hotelId)
                                       .ToListAsync();
            return _mapper.Map<IEnumerable<HotelImageDTO>>(images);
        }

        public async Task<HotelImageDTO> UploadImageAsync(HotelImageDTO hotelImageDto)
        {
            // Tạo thực thể HotelImage từ DTO
            var hotelImage = _mapper.Map<HotelImage>(hotelImageDto);

            // Gán URL của ảnh đã được lưu (đã có từ SaveImageAsync)
            hotelImage.ImageUrl = hotelImageDto.ImageUrl;

            // Thêm thực thể vào DbContext để lưu trữ
            _context.HotelImages.Add(hotelImage);
            await _context.SaveChangesAsync();

            // Trả về DTO sau khi lưu thành công
            return _mapper.Map<HotelImageDTO>(hotelImage);
        }


        public async Task<bool> DeleteImageAsync(int id)
        {
            var hotelImage = await _context.HotelImages.FindAsync(id);
            if (hotelImage == null)
                return false;

            _context.HotelImages.Remove(hotelImage);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<string> SaveImageAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

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
            return Path.Combine("images", uniqueFileName).Replace("\\", "/");
        }
        /*  public async Task<string> SaveImageAsync(IFormFile file)
          {
              if (file.Length > 0)
              {
                  // Tạo đường dẫn để lưu ảnh, ví dụ như trong thư mục "wwwroot/images"
                  var filePath = Path.Combine("wwwroot/images", file.FileName);

                  // Sử dụng FileStream để lưu ảnh vào hệ thống
                  using (var stream = new FileStream(filePath, FileMode.Create))
                  {
                      await file.CopyToAsync(stream);
                  }

                  // Trả về đường dẫn tương đối của ảnh để lưu trong cơ sở dữ liệu
                  return "/images/" + file.FileName;
              }

              return null;
          }*/

    }
}
