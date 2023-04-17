using System.ComponentModel.DataAnnotations;

namespace Roommates.Domain.Base
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModifiedDate { get; set; }
    }
}
