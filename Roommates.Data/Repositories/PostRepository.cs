using Roommates.Data.IRepositories;
using Roommates.Domain.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Data.Repositories
{
    public class PostRepository : BaseRepository<Post, RoommatesDbContext>, IPostRepository
    {
        public PostRepository(RoommatesDbContext dbContext) : base(dbContext)
        {
        }
    }
}
