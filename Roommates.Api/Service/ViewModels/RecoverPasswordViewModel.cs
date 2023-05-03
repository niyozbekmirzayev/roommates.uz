using System.ComponentModel.DataAnnotations;

namespace Roommates.Api.Service.ViewModels
{
    public class RecoverPasswordViewModel
    {
        [Required(ErrorMessage = "Verificatin code is required")]
        public string VerificatinCode { get; set; }

        [Required(ErrorMessage = "New password is required")]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
