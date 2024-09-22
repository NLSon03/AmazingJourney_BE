using System.ComponentModel.DataAnnotations;

namespace AmazingJourney_BE.AmazingJourney.Domain.Entities
{
    public class Payment
    {
        [Required]
        public int Id { get; set; }
        public int BookingId { get; set; }

        [StringLength(50)]
        public string? PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Status { get; set; }

        // Navigation property
        public Booking? Booking { get; set; }
    }
}
