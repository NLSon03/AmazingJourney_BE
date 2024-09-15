namespace AmazingJourney_BE.AmazingJourney.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        // Navigation properties
        public ICollection<Hotel> Hotels { get; set; }
    }
}
