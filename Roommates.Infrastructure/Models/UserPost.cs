using Roommates.Infrastructure.Base;
using Roommates.Infrastructure.Enums;
using System;
using System.Collections.Generic;
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

        public UserPostRelationType UserPostRelationType { get; set; }


        #region ForeignKeys

        [NotMapped]
        public User User { get; set; }

        [NotMapped]
        public Post Post { get; set;  } 

        #endregion
    }
}
