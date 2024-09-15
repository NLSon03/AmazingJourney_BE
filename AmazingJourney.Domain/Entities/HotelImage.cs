using System.ComponentModel.DataAnnotations;

namespace AmazingJourney_BE.AmazingJourney.Domain.Entities
{
    public class HotelImage
    {
        [Required]
        public int Id { get; set; }
        public int? HotelId { get; set; }
        public string? ImageUrl { get; set; }

        // Navigation property
        public Hotel? Hotel { get; set; }
    }
}
