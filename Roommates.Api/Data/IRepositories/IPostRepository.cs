using Roommates.Infrastructure.Models;

namespace Roommates.Api.Data.IRepositories
{
    public interface IPostRepository : IBaseRepository<Post, ApplicationDbContext>
    {
    }
}
