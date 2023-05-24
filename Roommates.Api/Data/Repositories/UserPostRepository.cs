using Roommates.Api.Data.IRepositories;
using Roommates.Infrastructure.Models;

namespace Roommates.Api.Data.Repositories
{
    public class UserPostRepository : BaseRepository<UserPost, ApplicationDbContext>, IUserPostRepository
    {
        public UserPostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
