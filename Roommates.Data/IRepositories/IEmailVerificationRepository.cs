using Roommates.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Data.IRepositories
{
    public interface IEmailVerificationRepository : IBaseRepository<EmailVerification, ApplicationDbContext>
    {
    }
}
