using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Roommates;
using Roommates.Infrastructure.Base;
using Roommates.Infrastructure.Enums;
using Roommates.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using File = Roommates.Infrastructure.Models.File;

namespace Roommates.Infrastructure.Models
{
    public class User : BaseModel, IEntityPersistent
    {
        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        [Column(TypeName = "varchar(24)")]
        public Gender? Gender { get; set; }

        public DateTime? Birthdate { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? PhoneNumberVerifiedDate { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        public DateTime? EmailVerifiedDate { get; set; }

        [Required] // Now required, but later will not be.....
        public string Password { get; set; }

        public string? Bio { get; set; }

        [ForeignKey(nameof(ProfilePicture))]
        public Guid? ProfilePictureFileId { get; set; }

        [Required]
        [Column(TypeName = "varchar(24)")]
        public EntityState EntityState { get; set; } = EntityState.Active;

        #region ForeignKeys

        public List<UserPost> RelatedPosts { get; set; }

        public List<Email> EmailVerifications { get; set; }

        public virtual File? ProfilePicture { get; set; }

        public DateTime? InactivatedDate { get; set; }

        #endregion
    }
}
