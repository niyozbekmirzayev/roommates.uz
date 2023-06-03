using System.ComponentModel.DataAnnotations;

namespace Roommates.Api.ViewModels
{
    public class RecoverPasswordViewModel
    {
        [Required]
        public string VerificatinCode { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
