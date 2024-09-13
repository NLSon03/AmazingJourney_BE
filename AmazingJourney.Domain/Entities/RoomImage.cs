namespace AmazingJourney_BE.AmazingJourney.Domain.Entities
{
    public class RoomImage
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string ImageUrl { get; set; }

        // Navigation property
        public Room Room { get; set; }
    }
}
