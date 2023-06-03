using Roommates.Api.Data.IRepositories;
using Roommates.Api.Data.IRepositories.Base;

namespace Roommates.Api.Data.Repositories.Base
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        public UnitOfWorkRepository(
            IFileRepository fileRepository,
            IPostRepository postRepository,
            IUserRepository userRepository,
            IEmailRepository emailRepository,
            IFilePostRepository filePostRepository,
            IDynamicFeatureRepository dynamicFeatureRepository,
            IStaticFeaturesRepository staticFeaturesRepository,
            ILocationRepository locationRepository)
        {
            FileRepository = fileRepository;
            PostRepository = postRepository;
            LocationRepository = locationRepository;
            UserRepository = userRepository;
            EmailRepository = emailRepository;
            FilePostRepository = filePostRepository;
            DynamicFeatureRepository = dynamicFeatureRepository;
            StaticFeaturesRepository = staticFeaturesRepository;
        }

        public IFileRepository FileRepository { get; }
        public ILocationRepository LocationRepository { get; }
        public IPostRepository PostRepository { get; }
        public IUserRepository UserRepository { get; }
        public IEmailRepository EmailRepository { get; }

        public IFilePostRepository FilePostRepository { get; }

        public IStaticFeaturesRepository StaticFeaturesRepository { get; }

        public IDynamicFeatureRepository DynamicFeatureRepository { get; }
    }
}
