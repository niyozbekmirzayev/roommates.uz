using Roommates.Data.IRepositories;
using Roommates.Domain.Models.Roommates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Data.Repositories
{

    public class RoommateRepository : BaseRepository<Roommate, RoommatesDbContext>, IRoommateRepository
    {
        public RoommateRepository(RoommatesDbContext dbContext) : base(dbContext)
        {
        }
    }
}
