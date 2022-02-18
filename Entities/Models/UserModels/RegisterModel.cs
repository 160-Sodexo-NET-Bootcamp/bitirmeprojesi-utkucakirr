using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class RegisterModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "{0} field at least {1}, max {2} chars", MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} field max {1}, at least {2} chars", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords not same")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} field max {1},  at least {2} chars.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} field max {1},  at least {2} chars.", MinimumLength = 2)]
        public string LastName { get; set; }
    }
}
