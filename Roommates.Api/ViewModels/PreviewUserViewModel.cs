namespace Roommates.Api.ViewModels
{
    public class PreviewUserViewModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public Guid? ProfilePictureFileId { get; set; }
    }
}
