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
    public class User : BaseModel, IPersistentEntity
    {
        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

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

        [Required]
        public EntityState EntityState { get; set; } = EntityState.Active;

        #region ForeignKeys

        [ForeignKey(nameof(ProfilePicture))]
        public Guid? ProfilePictureFileId { get; set; }

        public List<Post> LikedPosts { get; set; }

        public List<Post> OwnPosts { get; set; }

        public List<Email> EmailVerifications { get; set; }

        [NotMapped]
        public File? ProfilePicture { get; set; }
        public DateTime? InactivatedDate { get; set; }

        #endregion
    }
}
