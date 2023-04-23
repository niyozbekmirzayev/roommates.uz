using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Roommates;
using Roommates.Domain;
using Roommates.Domain.Base;
using Roommates.Domain.Enums;
using Roommates.Domain.Models;
using Roommates.Domain.Models.Files;
using Roommates.Domain.Models.Posts;
using Roommates.Domain.Models.Roommates;
using Roommates.Domain.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using File = Roommates.Domain.Models.Files.File;

namespace Roommates.Domain.Models.Roommates
{
    public class User : BaseModel, IPersistentEntity
    {
        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public Gender? Gender { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        public bool IsPhoneNumberVerified { get; set; } = false;

        [Required]
        public string Email { get; set; }

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

        public List<EmailVerification> EmailVerifications { get; set; }

        [NotMapped]
        public File? ProfilePicture { get; set; }

        #endregion
    }
}
