using Roommates.Domain.Base;

namespace Roommates.Domain.Models
{
    public class Roommate : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string IsVerified { get; set; }

        public string Bio { get; set; }
        public Media ProfilePicture { get; set; }

        #region ForeignKeys
        public List<Post> LikedPosts { get; set; }
        public List<Post> OwnPosts { get;}
        #endregion
    }
}
