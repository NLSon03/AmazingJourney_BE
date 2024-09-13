namespace AmazingJourney_BE.AmazingJourney.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }  // Rating scale of 1-5
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Hotel Hotel { get; set; }
        public User User { get; set; }
    }
}
