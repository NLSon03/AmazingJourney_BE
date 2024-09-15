using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazingJourney.Application.DTOs;

namespace AmazingJourney.Application.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDTO>> GetAllLocationsAsync();
        Task<LocationDTO> GetLocationByIdAsync(int id);
        Task<LocationDTO> CreateLocationAsync(LocationDTO locationDto);
        Task<LocationDTO> UpdateLocationAsync(int id, LocationDTO locationDto);
        Task<bool> DeleteLocationAsync(int id);
    }
}
