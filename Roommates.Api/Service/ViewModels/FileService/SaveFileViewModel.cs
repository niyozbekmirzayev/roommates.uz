using Roommates.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace Roommates.Api.Service.ViewModels.FileService
{
    public class SaveFileViewModel
    {
        [Required]
        public Guid FileId { get; set; }

        [Required]
        public ActionType ActionType { get; set; }
    }
}
