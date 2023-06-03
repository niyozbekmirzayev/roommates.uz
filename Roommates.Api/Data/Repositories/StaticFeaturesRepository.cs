using Roommates.Api.Data.IRepositories;
using Roommates.Api.Data.Repositories.Base;
using Roommates.Infrastructure.Models;

namespace Roommates.Api.Data.Repositories
{
    public class StaticFeaturesRepository : BaseRepository<StaticFeatures, ApplicationDbContext>, IStaticFeaturesRepository
    {
        public StaticFeaturesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
