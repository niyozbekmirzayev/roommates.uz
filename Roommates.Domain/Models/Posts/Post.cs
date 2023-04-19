using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Roommates;
using Roommates.Domain;
using Roommates.Domain.Base;
using Roommates.Domain.Enums;
using Roommates.Domain.Models;
using Roommates.Domain.Models.Locations;
using Roommates.Domain.Models.Posts;
using Roommates.Domain.Models.Roommates;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roommates.Domain.Models.Posts
{
    public class Post : BaseModel, IPersistentEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        PostType PostType { get; set; } = PostType.Apartment;

        [Required]
        public string Description { get; set; }

        [Required]
        public Location Location { get; set; }

        public string? Address { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public short RoomsCount { get; set; }

        [Required]
        public bool IsForSelling { get; set; } = false;

        [Required]
        public long ViewedTime { get; set; } = 0;

        [Required]
        public Gender PreferedRoommateGender { get; set; } = Gender.NotSpecified;

        public PricePeriodType? PricePeriodType { get; set; }

        [Required]
        public CurrencyType CurrencyType { get; set; }

        [Required]
        public Guid CreatedByRoommateId { get; set; }

        [Required]
        public EntityState EntityState { get; set; } = EntityState.Active;

        #region ForeignKeys

        [NotMapped]
        public Roommate CreatedByRoommate { get; set; }

        public List<Roommate>? LikedByRoommates { get; set; }

        public List<FilesPosts>? AppartmentViewFiles { get; set; }

        #endregion
    }
}
