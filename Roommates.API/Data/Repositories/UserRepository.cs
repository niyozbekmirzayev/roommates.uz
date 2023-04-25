using Roommates.Infrastructure.Models;

namespace Roommates.Api.Data.Repositories
{
    public class UserRepository : BaseRepository<User, ApplicationDbContext>, IRepositories.IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
