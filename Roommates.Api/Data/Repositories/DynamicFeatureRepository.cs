using Roommates.Api.Data.IRepositories;
using Roommates.Api.Data.Repositories.Base;
using Roommates.Infrastructure.Models;

namespace Roommates.Api.Data.Repositories
{
    public class DynamicFeatureRepository : BaseRepository<DynamicFeature, ApplicationDbContext>, IDynamicFeatureRepository
    {
        public DynamicFeatureRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
