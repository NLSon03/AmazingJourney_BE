
using System.ComponentModel.DataAnnotations;

namespace AmazingJourney_BE.AmazingJourney.Domain.Entities
{
    
    public class Payment
    {
        [Required]
        public int Id { get; set; }
        public int BookingId { get; set; }

        [Required]
        public string Method { get; set; }  
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Status { get; set; }

        // Navigation property
        public Booking? Booking { get; set; }
    }
}
