using Roommates.Api.Data.IRepositories;
using Roommates.Infrastructure.Models;

namespace Roommates.Api.Data.Repositories
{
    public class PostRepository : BaseRepository<Post, ApplicationDbContext>, IPostRepository
    {
        public PostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
