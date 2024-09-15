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
            var hotels = await _context.Hotels.ToListAsync();

            if(hotels == null)
            {
                return null;
            }
            return _mapper.Map<IEnumerable<HotelDTO>>(hotels);
        }

        public async Task<HotelDTO> GetHotelByIdAsync(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            return hotel == null ? null : _mapper.Map<HotelDTO>(hotel);
        }

        public async Task<HotelDTO> CreateHotelAsync(HotelDTO hotelDto)
        {
            var hotel = _mapper.Map<Hotel>(hotelDto);
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return _mapper.Map<HotelDTO>(hotel);
        }

        public async Task<HotelDTO> UpdateHotelAsync(int id, HotelDTO hotelDto)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
                return null;

            _mapper.Map(hotelDto, hotel);
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();

            return _mapper.Map<HotelDTO>(hotel);
        }

        public async Task<bool> DeleteHotelAsync(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
                return false;

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<HotelDTO>> GetHotelByNameAsync(string name)
        {
            var hotels = await _context.Hotels
                .Where(h => h.Name.Contains(name))
                .ToListAsync();
            return _mapper.Map<IEnumerable<HotelDTO>>(hotels);
        }
        public async Task<IEnumerable<HotelDTO>> GetListHotelByCategoryId(int categoryId)
        {
            var hotels = await _context.Hotels
                .Where(h => h.CategoryId == categoryId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<HotelDTO>>(hotels);
        }

        public async Task<IEnumerable<HotelDTO>> GetListHotelByLocationId(int locationId)
        {
            var hotels = await _context.Hotels
                .Where(h => h.LocationId == locationId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<HotelDTO>>(hotels);
        }

      
    }
}
