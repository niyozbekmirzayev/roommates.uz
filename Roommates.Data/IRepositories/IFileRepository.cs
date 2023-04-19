using Roommates.Domain.Models.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Roommates.Domain.Models.Files.File;

namespace Roommates.Data.IRepositories
{
    public interface IFileRepository : IBaseRepository<File, RoommatesDbContext>
    {
    }
}
