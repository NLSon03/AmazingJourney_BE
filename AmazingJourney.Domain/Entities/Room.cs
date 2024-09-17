using System.ComponentModel.DataAnnotations;

namespace AmazingJourney_BE.AmazingJourney.Domain.Entities
{
    public class Room
    {
        [Required]
        public int Id { get; set; }
        public int HotelId { get; set; }

        [Required]
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool Availability { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Foreign Key for Hotel
        public Hotel?  Hotel { get; set; }

        // Navigation properties
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<RoomImage>? RoomImages { get; set; }
    }
}
