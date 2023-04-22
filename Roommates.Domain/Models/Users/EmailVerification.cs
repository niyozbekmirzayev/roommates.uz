﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Roommates;
using Roommates.Domain;
using Roommates.Domain.Base;
using Roommates.Domain.Models;
using Roommates.Domain.Models.Roommates;
using Roommates.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Domain.Models.Users
{
    public class EmailVerification : BaseModel
    {
        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string VerificationCode { get; set; } = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();

        [Required]
        public DateTime ExpirationDate = DateTime.UtcNow.AddHours(1);

        [NotMapped]
        public User User { get; set; }
    }
}