using Roommates.Api.Data.IRepositories.Base;
using Roommates.Infrastructure.Models;

namespace Roommates.Api.Data.IRepositories
{
    public interface IDynamicFeatureRepository : IBaseRepository<DynamicFeature, ApplicationDbContext>
    {
    }
}
