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
    public class DynamicFeature : BaseModel
    {
        [Required]
        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(24)")]
        public DynamicFeatureType DynamicFeatureType { get; set; }

        public long? Count { get; set; }

        public bool? IsExist { get; set; }

        #region ForeignKeys

        public virtual Post Post { get; set; }

        #endregion
    }
}
