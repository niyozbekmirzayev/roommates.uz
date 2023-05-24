using Newtonsoft.Json.Converters;
using Roommates;
using Roommates.Infrastructure.Base;
using Roommates.Infrastructure.Enums;
using Roommates.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roommates.Infrastructure.Models
{
    public class File : BaseModel, IPersistentEntity
    {
        [Required]
        public string Name { get; set; }

        public string? Extension { get; set; }

        public byte[]? Content { get; set; }

        public string? MimeType { get; set; }

        [Required]
        public EntityState EntityState { get; set; } = EntityState.Active;

        public DateTime? InactivatedDate { get; set; }

        [Required]
        [ForeignKey(nameof(AuthorUser))]
        public Guid AuthorUserId { get; set; }

        [Required]
        public bool IsTemporary { get; set; } = false;

        #region ForeignKeys

        public virtual User AuthorUser { get; set; }

        #endregion
    }
}
