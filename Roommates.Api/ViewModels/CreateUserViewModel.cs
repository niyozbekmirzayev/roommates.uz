﻿using Roommates.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace Roommates.Api.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        //[MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }

        public Gender? Gender { get; set; }

        public string Bio { get; set; }

        public DateTime? Brithdate { get; set; }
    }
}
