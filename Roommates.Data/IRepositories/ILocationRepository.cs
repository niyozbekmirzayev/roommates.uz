using Roommates.Domain.Models.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Data.IRepositories
{
    public interface ILocationRepository : IBaseRepository<Location, ApplicationDbContext>
    {
    }
}
