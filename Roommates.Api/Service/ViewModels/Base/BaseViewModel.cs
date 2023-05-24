
namespace Roommates.Api.Service.ViewModels.Base
{
    public class BaseViewModel
    {
        public Guid Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
