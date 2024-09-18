using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingJourney.Application.DTOs
{
    public class RoomDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int HotelId { get; set; }

        [Required]
        public string RoomType { get; set; }

        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool Availability { get; set; }

        // Thêm danh sách các ảnh liên quan đến phòng
        public List<RoomImageDTO> Images { get; set; } = new List<RoomImageDTO>();
    }
}
