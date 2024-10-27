using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazingJourney.Application.DTOs;
using AmazingJourney_BE.AmazingJourney.Domain.Entities;
using AmazingJourney_BE.AmazingJourney.DTOs;
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

            CreateMap<HotelDTO, Hotel>()
      // Chỉ map Rooms nếu Rooms != null và các giá trị hợp lệ
      .ForMember(dest => dest.Rooms, opt => opt.Condition(src => src.Rooms != null && src.Rooms.Any(r => r.Id != 0)))
      // Chỉ map HotelImages nếu Images != null và các giá trị hợp lệ
      .ForMember(dest => dest.HotelImages, opt => opt.Condition(src => src.Images != null && src.Images.Any(img => img.Id != 0)));  // Map HotelImages

            CreateMap<Hotel, HotelDTO>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.HotelImages))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms));  // Map danh sách phòng khi get


            CreateMap<HotelImage, HotelImageDTO>();
            CreateMap<HotelImageDTO, HotelImage>();


            // Mapping Room và RoomDTO
            CreateMap<Room, RoomDTO>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.RoomImages));  // Map danh sách ảnh

            CreateMap<RoomDTO, Room>();

            // Mapping RoomImage và RoomImageDTO
            CreateMap<RoomImage, RoomImageDTO>();
            CreateMap<RoomImageDTO, RoomImage>();
            // Ánh xạ từ BookingCreateDTO sang Booking
            CreateMap<BookingCreateDTO, Booking>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // UserId sẽ được lấy từ người dùng đăng nhập
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // Ánh xạ từ Booking sang BookingDTO
            CreateMap<Booking, BookingDTO>();
        }
    }
}
