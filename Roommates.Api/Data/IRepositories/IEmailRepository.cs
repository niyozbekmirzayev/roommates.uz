using Roommates.Api.Data.IRepositories.Base;
using Roommates.Infrastructure.Models;

namespace Roommates.Api.Data.IRepositories
{
    public interface IEmailRepository : IBaseRepository<Email, ApplicationDbContext>
    {
    }
}
