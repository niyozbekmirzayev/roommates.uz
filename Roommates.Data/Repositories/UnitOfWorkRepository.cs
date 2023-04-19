using Roommates.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Data.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        public UnitOfWorkRepository(
            IFileRepository fileRepository,
            IPostRepository postRepository,
            IRoommateRepository roommateRepository,
            ILocationRepository locationRepository)
        {
            FileRepository = fileRepository;
            PostRepository = postRepository;
            LocationRepository = locationRepository;
            RoommateRepository = roommateRepository;
        }

        public IFileRepository FileRepository { get; }
        public ILocationRepository LocationRepository { get; }
        public IPostRepository PostRepository { get; }
        public IRoommateRepository RoommateRepository { get; }
    }
}
