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

            // Hotel mapping
            CreateMap<Hotel, HotelDTO>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.HotelImages))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms));  // Map phòng

            CreateMap<HotelImage, HotelImageDTO>();
            CreateMap<HotelDTO, Hotel>();
            CreateMap<HotelImageDTO, HotelImage>();


            // Mapping Room và RoomDTO
            CreateMap<Room, RoomDTO>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.RoomImages));  // Map danh sách ảnh

            CreateMap<RoomDTO, Room>();

            // Mapping RoomImage và RoomImageDTO
            CreateMap<RoomImage, RoomImageDTO>();
            CreateMap<RoomImageDTO, RoomImage>();
        }
    }
}
