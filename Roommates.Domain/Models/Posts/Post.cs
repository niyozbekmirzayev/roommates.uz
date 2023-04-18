using Newtonsoft.Json.Converters;
using Roommates;
using Roommates.Domain;
using Roommates.Domain.Base;
using Roommates.Domain.Enums;
using Roommates.Domain.Models;
using Roommates.Domain.Models.Posts;
using Roommates.Domain.Models.Roommates;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Roommates.Domain.Models.Posts
{
    public class Post : BaseModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        PostType PostType { get; set; } = PostType.Apartment;

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsForSelling { get; set; } = false;

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public PricePeriodType? PricePeriodType { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public CurrencyType CurrencyType { get; set; }

        [Required]
        [ForeignKey(nameof(CreatedByRoommate))]
        public Guid CreatedByRoommateId { get; set; }

        #region ForeignKeys
        [NotMapped]
        public Roommate CreatedByRoommate { get; set; }

        public List<Roommate>? LikedByRoommates { get; set; }

        public List<FilesPosts>? AppartmentViewFiles { get; set; }

        #endregion
    }
}
