using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingJourney.Application.DTOs
{
    public class HotelImageDTO
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int HotelId { get; set; }
    }
}
