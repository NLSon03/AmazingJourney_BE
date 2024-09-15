using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazingJourney.Application.DTOs;
using AmazingJourney_BE.AmazingJourney.Domain.Entities;
using AutoMapper;

namespace AmazingJourney.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map CategoryDTO to Category entity and vice versa
            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();

            // Map LocationDTO to Location entity and vice versa
            CreateMap<LocationDTO, Location>();
            CreateMap<Location, LocationDTO>();

            // Map HotelDTO to Hotel entity and vice versa
            CreateMap<HotelDTO, Hotel>();
            CreateMap<Hotel, HotelDTO>();

            // Map HotelImageDTO to HotelImage entity and vice versa
            CreateMap<HotelImageDTO, HotelImage>();
            CreateMap<HotelImage, HotelImageDTO>();
        }
    }
}
