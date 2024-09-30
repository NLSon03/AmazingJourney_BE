using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AmazingJourney_BE.AmazingJourney.Domain.Entities
{
    public class User : IdentityUser
    {
        [StringLength(50)]
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    /*public class User
    {
        [Required]
        public string Id { get; set; }

        [StringLength(50)]
        public string? Username { get; set; }
        public string?  Password { get; set; }
        public string? FullName {  get; set; }
        [StringLength(50)]
        public string? Email { get; set; }

        [StringLength(11)]
        public string? Phone { get; set; }

        [StringLength(20)]
        public string? Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }*/
}
