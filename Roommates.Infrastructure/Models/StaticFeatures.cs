using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Roommates.Infrastructure.Base;
using Roommates.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Infrastructure.Models
{
    public class StaticFeatures : BaseModel, IPersistentEntity
    {
        [Required]
        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public short RoomsCount { get; set; }

        [Required]
        public bool IsForSelling { get; set; } = false;

        [Required]
        [Column(TypeName = "varchar(24)")]
        public ClientType PreferedClientType { get; set; } = ClientType.All;

        [Column(TypeName = "varchar(24)")]
        public PricePeriodType? PricePeriodType { get; set; }

        [Required]
        [Column(TypeName = "varchar(24)")]
        public CurrencyType CurrencyType { get; set; }

        [Required]
        [Column(TypeName = "varchar(24)")]
        public EntityState EntityState { get; set; }

        public DateTime? InactivatedDate { get; set; }

        #region ForeignKeys

        public virtual Post Post { get; set; }

        #endregion
    }
}
