using Roommates;
using Roommates.Domain;
using Roommates.Domain.Base;
using Roommates.Domain.Models;
using Roommates.Domain.Models.Files;
using Roommates.Domain.Models.Posts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roommates.Domain.Models.Posts
{
    public class FilesPosts : BaseModel
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
