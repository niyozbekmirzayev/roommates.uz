using Roommates.Api.Data.IRepositories;
using Roommates.Infrastructure.Models;

namespace Roommates.Api.Data.Repositories
{
    public class FilePostRepository : BaseRepository<FilePost, ApplicationDbContext>, IFilePostRepository
    {
        public FilePostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
