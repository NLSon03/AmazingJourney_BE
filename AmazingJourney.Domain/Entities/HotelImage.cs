namespace AmazingJourney_BE.AmazingJourney.Domain.Entities
{
    public class HotelImage
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string ImageUrl { get; set; }

        // Navigation property
        public Hotel Hotel { get; set; }
    }
}
