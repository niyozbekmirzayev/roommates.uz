using System.ComponentModel.DataAnnotations;

namespace Roommates.Service.ViewModels
{
    public class UpdatePasswordViewModel
    {
        [Required(ErrorMessage = "Old is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Old is required")]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
