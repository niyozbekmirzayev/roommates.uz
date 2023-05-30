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
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "varchar(24)")]
        PostType PostType { get; set; } = PostType.Apartment;

        [Required]
        [ForeignKey(nameof(Location))]
        public Guid LocationId { get; set; }

        [Required]
        public long ViewedCount { get; set; } = 0;

        [Required]
        [ForeignKey(nameof(CreatedByUser))]
        public Guid CreatedByUserId { get; set; }

        [Required]
        [Column(TypeName = "varchar(24)")]
        public EntityState EntityState { get; set; } = EntityState.Active;

        [Required]
        [Column(TypeName = "varchar(24)")]
        public PostStatus PostStatus { get; set; } = PostStatus.Active;

        public DateTime? InactivatedDate { get; set; }

        #region ForeignKeys

        public List<DynamicFeature> DynamicFeatures { get; set; }

        public virtual User CreatedByUser { get; set; }

        public virtual StaticFeatures StaticFeatures { get; set; }

        public virtual Location Location { get; set; }

        public List<FilePost> AppartmentViewFiles { get; set; }

        #endregion
    }
}
