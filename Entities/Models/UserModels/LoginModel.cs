using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [StringLength(20, ErrorMessage = "{0} field max {1}, at least {2} chars", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
