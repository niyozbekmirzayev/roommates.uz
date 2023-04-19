using Roommates.Data.IRepositories;
using Roommates.Domain.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Data.Repositories
{
    public class LocationRepository : BaseRepository<Location, RoommatesDbContext>, ILocationRepository
    {
        public LocationRepository(RoommatesDbContext dbContext) : base(dbContext)
        {
        }
    }
}
