using Roommates.Api.Data.IRepositories;
using Roommates.Api.Data.Repositories.Base;
using Roommates.Infrastructure.Models;

namespace Roommates.Api.Data.Repositories
{
    public class EmailRepository : BaseRepository<Email, ApplicationDbContext>, IEmailRepository
    {
        public EmailRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
