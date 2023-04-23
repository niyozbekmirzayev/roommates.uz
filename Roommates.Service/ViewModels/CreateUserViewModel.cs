using Roommates.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Roommates.Service.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        //[MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }

        public Gender? Gender { get; set; }

        public string Bio { get; set; }
    }
}
