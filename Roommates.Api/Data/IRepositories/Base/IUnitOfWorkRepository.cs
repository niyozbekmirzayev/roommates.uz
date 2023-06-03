namespace Roommates.Api.Data.IRepositories.Base
{
    public interface IUnitOfWorkRepository
    {
        public IFileRepository FileRepository { get; }
        public ILocationRepository LocationRepository { get; }
        public IPostRepository PostRepository { get; }
        public IUserRepository UserRepository { get; }
        public IEmailRepository EmailRepository { get; }
        public IFilePostRepository FilePostRepository { get; }

        public IDynamicFeatureRepository DynamicFeatureRepository { get; }

        public IStaticFeaturesRepository StaticFeaturesRepository { get; }
    }
}
