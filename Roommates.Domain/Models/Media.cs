using Roommates.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Roommates.Domain.Models
{
    public class Media : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Extension { get;set; }

        public byte[] Data { get; set; }
    }
}
