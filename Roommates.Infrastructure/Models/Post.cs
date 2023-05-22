using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Roommates;
using Roommates.Infrastructure.Base;
using Roommates.Infrastructure.Enums;
using Roommates.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roommates.Infrastructure.Models
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
        [ForeignKey(nameof(Location))]
        public Guid LocationId { get; set; }

        public string? Address { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public short RoomsCount { get; set; }

        [Required]
        public bool IsForSelling { get; set; } = false;

        [Required]
        public long ViewedCount { get; set; } = 0;

        [Required]
        public Gender PreferedUserGender { get; set; } = Gender.NotSpecified;

        public PricePeriodType? PricePeriodType { get; set; }

        [Required]
        public CurrencyType CurrencyType { get; set; }

        [Required]
        public Guid CreatedByUserId { get; set; }

        [Required]
        public EntityState EntityState { get; set; } = EntityState.Active;

        public DateTime? InactivatedDate { get; set; }

        #region ForeignKeys

        [NotMapped]
        public User CreatedByUser { get; set; }

        public List<FilePost> AppartmentViewFiles { get; set; }

        [NotMapped]
        public Location Location { get; set; }

        #endregion
    }
}
