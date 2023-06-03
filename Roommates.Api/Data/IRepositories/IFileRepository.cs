using Roommates.Api.Data.IRepositories.Base;
using File = Roommates.Infrastructure.Models.File;

namespace Roommates.Api.Data.IRepositories
{
    public interface IFileRepository : IBaseRepository<File, ApplicationDbContext>
    {
    }
}
