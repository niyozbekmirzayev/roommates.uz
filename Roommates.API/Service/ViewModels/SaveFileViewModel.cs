using Roommates.Infrastructure.Enums;

namespace Roommates.Api.Service.ViewModels
{
    public class SaveFileViewModel
    {
        public Guid FileId { get; set; }
        public ActionType ActionType { get; set; }
    }
}
