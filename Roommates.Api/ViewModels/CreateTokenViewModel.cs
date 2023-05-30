using System.ComponentModel.DataAnnotations;

namespace Roommates.Api.ViewModels
{
    public class CreateTokenViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
