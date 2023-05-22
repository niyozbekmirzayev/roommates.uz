using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Roommates;
using Roommates.Infrastructure.Base;
using Roommates.Infrastructure.Enums;
using Roommates.Infrastructure.Models;
using System;  
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Infrastructure.Models
{
    public class Email : BaseModel
    {
        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string VerificationCode { get; set; } = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();

        [Required]
        public DateTime ExpirationDate = DateTime.UtcNow.AddHours(1);

        [Required]
        public EmailType Type { get; set; }
          
        public DateTime? VerifiedDate { get; set; }

        [NotMapped]
        public User User { get; set; }
    }
}
