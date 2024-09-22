using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazingJourney.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace AmazingJourney.Application.Interfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelDTO>> GetAllHotelsAsync();
        Task<HotelDTO> GetHotelByIdAsync(int id);
        Task<HotelDTO> GetHotelWithImagesByIdAsync(int hotelId);
        Task<IEnumerable<HotelDTO>> GetHotelByNameAsync(string name);   

        Task<IEnumerable<HotelDTO>> GetListHotelByCategoryId(int categoryId);

        Task<IEnumerable<HotelDTO>> GetListHotelByLocationId(int locationId);
       
        Task<HotelDTO> CreateHotelAsync(HotelDTO hotelDto);
        Task<HotelDTO> UpdateHotelAsync(int id, HotelDTO hotelDto);
        Task<bool> DeleteHotelAsync(int id);

        Task<string> SaveImageAsync(IFormFile file);

        Task  AddHotelImageAsync(HotelImageDTO hotelImageDto);
    }
}
