using Roommates.Api.Data.IRepositories;
using Roommates.Api.Data.Repositories.Base;
using Roommates.Infrastructure.Models;

namespace Roommates.Api.Data.Repositories
{
    public class LocationRepository : BaseRepository<Location, ApplicationDbContext>, ILocationRepository
    {
        public LocationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
