using Roommates.Data.IRepositories;
using Roommates.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Data.Repositories
{
    public class EmailVerificationRepository : BaseRepository<EmailVerification, ApplicationDbContext>, IEmailVerificationRepository
    {
        public EmailVerificationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
