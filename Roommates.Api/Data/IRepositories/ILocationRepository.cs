using Roommates.Infrastructure.Models;

namespace Roommates.Api.Data.IRepositories
{
    public interface ILocationRepository : IBaseRepository<Location, ApplicationDbContext>
    {
    }
}
