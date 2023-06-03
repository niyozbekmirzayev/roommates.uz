using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Roommates.Infrastructure.Base;
using Roommates.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Infrastructure.Models
{
    public class UserPost : BaseModel
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }

        [Required]
        [Column(TypeName = "varchar(24)")]
        public UserPostRelationType UserPostRelationType { get; set; }

        #region ForeignKeys

        public virtual User User { get; set; }

        public virtual Post Post { get; set; }

        #endregion
    }
}
