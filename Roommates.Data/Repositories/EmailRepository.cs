using Roommates.Data.IRepositories;
using Roommates.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Data.Repositories
{
    public class EmailRepository : BaseRepository<Email, ApplicationDbContext>, IEmailRepository
    {
        public EmailRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
