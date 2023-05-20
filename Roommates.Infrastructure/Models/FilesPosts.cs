using Roommates;
using Roommates.Infrastructure.Base;
using Roommates.Infrastructure.Models;
using System.ComponentModel.DataAnnotations.Schema;
using File = Roommates.Infrastructure.Models.File;

namespace Roommates.Infrastructure.Models
{
    public class FilePost : BaseModel
    {
        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }

        [ForeignKey(nameof(File))]
        public Guid FileId { get; set; }

        #region ForeignKeys

        [NotMapped]
        public Post Post { get; set; }

        [NotMapped]
        public File File { get; set; }

        #endregion
    }
}
