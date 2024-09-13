namespace AmazingJourney_BE.AmazingJourney.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }

        // Navigation property
        public Booking Booking { get; set; }
    }
}
