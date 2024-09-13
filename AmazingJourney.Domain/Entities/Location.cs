namespace AmazingJourney_BE.AmazingJourney.Domain.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Nation { get; set; }
        public ICollection<Hotel> Hotels { get; set; }
    }
}
