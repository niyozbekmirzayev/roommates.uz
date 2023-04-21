using Roommates.Data.IRepositories;
using Roommates.Domain.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Data.Repositories
{
    public class PostRepository : BaseRepository<Post, ApplicationDbContext>, IPostRepository
    {
        public PostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
