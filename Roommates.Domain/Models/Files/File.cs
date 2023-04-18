using Roommates;
using Roommates.Domain;
using Roommates.Domain.Base;
using Roommates.Domain.Models;
using Roommates.Domain.Models.Files;
using Roommates.Domain.Models.Roommates;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roommates.Domain.Models.Files
{
    public class File : BaseModel
    {
        [Required]
        public string Name { get; set; }

        public string Extension { get; set; }

        public byte[] Content { get; set; }

        public string MimeType { get; set; }

        [Required]
        [ForeignKey(nameof(AuthorRoommate))]
        public Guid AuthorRoommateId { get; set; }

        #region ForeignKeys

        [NotMapped]
        public Roommate AuthorRoommate { get; set; }

        #endregion
    }
}
