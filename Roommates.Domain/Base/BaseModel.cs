using Newtonsoft.Json.Converters;
using Roommates.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Roommates.Domain.Base
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastModifiedDate { get; set; }
    }
}
