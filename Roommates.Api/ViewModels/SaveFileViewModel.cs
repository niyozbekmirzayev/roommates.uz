using Roommates.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace Roommates.Api.ViewModels
{
    public class SaveFileViewModel
    {
        [Required]
        public Guid FileId { get; set; }

        [Required]
        public ActionType ActionType { get; set; }

        public short? Sequence { get; set; }

        [Required]
        public bool IsMain { get; set; }
    }
}
