using Roommates.Api.ViewModels.Base;

namespace Roommates.Api.ViewModels
{
    public class PreviewUserViewModel : BaseViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public Guid? ProfilePictureFileId { get; set; }
    }
}
