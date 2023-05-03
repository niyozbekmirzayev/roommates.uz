using Roommates.Infrastructure.Models;

namespace Roommates.Api.Data.IRepositories
{
    public interface IUserRepository : IBaseRepository<User, ApplicationDbContext>
    {
    }
}
