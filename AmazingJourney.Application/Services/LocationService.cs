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
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LocationService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationDTO>> GetAllLocationsAsync()
        {
            var locations = await _context.Locations.ToListAsync();
            return _mapper.Map<IEnumerable<LocationDTO>>(locations);
        }

        public async Task<LocationDTO> GetLocationByIdAsync(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            return location == null ? null : _mapper.Map<LocationDTO>(location);
        }

        public async Task<LocationDTO> CreateLocationAsync(LocationDTO locationDto)
        {
            var location = _mapper.Map<Location>(locationDto);
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return _mapper.Map<LocationDTO>(location);
        }

        public async Task<LocationDTO> UpdateLocationAsync(int id, LocationDTO locationDto)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
                return null;

            _mapper.Map(locationDto, location);
            _context.Locations.Update(location);
            await _context.SaveChangesAsync();

            return _mapper.Map<LocationDTO>(location);
        }

        public async Task<bool> DeleteLocationAsync(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
                return false;

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
