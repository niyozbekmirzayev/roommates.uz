using Newtonsoft.Json.Converters;
using Roommates;
using Roommates.Domain;
using Roommates.Domain.Base;
using Roommates.Domain.Enums;
using Roommates.Domain.Models;
using Roommates.Domain.Models.Files;
using Roommates.Domain.Models.Posts;
using Roommates.Domain.Models.Roommates;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using File = Roommates.Domain.Models.Files.File;

namespace Roommates.Domain.Models.Roommates
{
    public class Roommate : BaseModel, IPersistentEntity
    {
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender? Gender { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsPhoneNumberVerified { get; set; } = false;

        public string Bio { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EntityState EntityState { get; set; } = EntityState.Active;

        #region ForeignKeys

        [ForeignKey(nameof(ProfilePicture))]
        public Guid? ProfilePictureFileId { get; set; }

        public List<Post>? LikedPosts { get; set; }

        public List<Post>? OwnPosts { get; set; }

        [NotMapped]
        public File? ProfilePicture { get; set; }

        #endregion
    }
}
