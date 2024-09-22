using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazingJourney.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace AmazingJourney.Application.Interfaces
{
    public interface IHotelImageService
    {
        Task<IEnumerable<HotelImageDTO>> GetImagesByHotelIdAsync(int hotelId);
        Task<HotelImageDTO> UploadImageAsync(HotelImageDTO hotelImageDto);
        Task<string> SaveImageAsync(IFormFile file);
        Task<bool> DeleteImageAsync(int id);
    }
}
