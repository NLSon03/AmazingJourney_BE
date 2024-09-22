using System.ComponentModel.DataAnnotations;

namespace AmazingJourney_BE.AmazingJourney.Domain.Entities
{
    public class Hotel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Address { get; set; }
        public int LocationId { get; set; }
        public Location? Locations { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; } // Rating scale of 1-5
        public string? Amenities { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


        // Foreign Key for Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        // Navigation property for rooms and reviews
        public ICollection<Room>? Rooms { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<HotelImage>? HotelImages { get; set; }
        
    }
}
