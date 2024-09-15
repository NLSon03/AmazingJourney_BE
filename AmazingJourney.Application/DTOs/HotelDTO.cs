using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingJourney.Application.DTOs
{
    public class HotelDTO
    {
        [Required]
        public int Id { get; set; }

      
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? LocationId { get; set; }
        public string? Description { get; set; }
        public double? Rating { get; set; }
        public string? Amenities { get; set; }

        public int? CategoryId { get; set; }
    }
}
