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

        [Required]
        public short Sequence { get; set; }
    }
}
