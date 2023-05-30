using Roommates;
using Roommates.Infrastructure.Base;
using Roommates.Infrastructure.Models;
using System.ComponentModel.DataAnnotations.Schema;
using File = Roommates.Infrastructure.Models.File;

namespace Roommates.Infrastructure.Models
{
    public class FilePost : BaseModel
    {
        public short Sequence { get; set; }

        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }

        [ForeignKey(nameof(File))]
        public Guid FileId { get; set; }

        #region ForeignKeys


        public virtual Post Post { get; set; }

        public virtual File File { get; set; }

        #endregion
    }
}
