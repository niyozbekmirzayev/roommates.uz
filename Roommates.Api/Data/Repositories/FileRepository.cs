using Roommates.Api.Data.IRepositories;
using Roommates.Api.Data.Repositories.Base;
using File = Roommates.Infrastructure.Models.File;

namespace Roommates.Api.Data.Repositories
{
    public class FileRepository : BaseRepository<File, ApplicationDbContext>, IFileRepository
    {
        public FileRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
