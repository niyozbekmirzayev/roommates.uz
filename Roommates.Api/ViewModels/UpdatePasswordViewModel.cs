using System.ComponentModel.DataAnnotations;

namespace Roommates.Api.ViewModels
{
    public class UpdatePasswordViewModel
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
