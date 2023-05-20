using Roommates.Api.Data.IRepositories;

namespace Roommates.Api.Data.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        public UnitOfWorkRepository(
            IFileRepository fileRepository,
            IPostRepository postRepository,
            IUserRepository userRepository,
            IEmailRepository emailRepository,
            IFilePostRepository filePostRepository,
            ILocationRepository locationRepository)
        {
            FileRepository = fileRepository;
            PostRepository = postRepository;
            LocationRepository = locationRepository;
            UserRepository = userRepository;
            EmailRepository = emailRepository;
            FilePostRepository = filePostRepository;
        }

        public IFileRepository FileRepository { get; }
        public ILocationRepository LocationRepository { get; }
        public IPostRepository PostRepository { get; }
        public IUserRepository UserRepository { get; }
        public IEmailRepository EmailRepository { get; }

        public IFilePostRepository FilePostRepository { get; }
    }
}
