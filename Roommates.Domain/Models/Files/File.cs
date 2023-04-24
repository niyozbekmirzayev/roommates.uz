﻿using Newtonsoft.Json.Converters;
using Roommates;
using Roommates.Domain;
using Roommates.Domain.Base;
using Roommates.Domain.Enums;
using Roommates.Domain.Models;
using Roommates.Domain.Models.Files;
using Roommates.Domain.Models.Roommates;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roommates.Domain.Models.Files
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

        #region ForeignKeys

        [NotMapped]
        public User AuthorUser { get; set; }

        #endregion
    }
}
