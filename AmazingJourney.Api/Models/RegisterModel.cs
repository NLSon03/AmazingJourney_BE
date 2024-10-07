using System.ComponentModel.DataAnnotations;

namespace AmazingJourney.Api.Models
{
    public class RegisterModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }
    }
}
