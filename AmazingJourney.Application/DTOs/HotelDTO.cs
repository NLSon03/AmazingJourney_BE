using System.Collections.Generic;

namespace AmazingJourney.Application.DTOs
{
    public class HotelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string Amenities { get; set; }
        public int CategoryId { get; set; }
        public List<HotelImageDTO> Images { get; set; } = new List<HotelImageDTO>(); // Danh sách các hình ảnh liên quan

        // Thêm danh sách các phòng liên quan
        public List<RoomDTO> Rooms { get; set; } = new List<RoomDTO>();
    }   

    /*
    public class HotelImageDTO
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int HotelId { get; set; }
    }*/
}
