using System.ComponentModel.DataAnnotations;

namespace AmazingJourney_BE.AmazingJourney.Domain.Entities
{
    public class Location
    {
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? Nation { get; set; }
        public ICollection<Hotel>? Hotels { get; set; }
    }
}
