namespace Roommates.Api.Data.IRepositories
{
    public interface IUnitOfWorkRepository
    {
        public IFileRepository FileRepository { get; }
        public ILocationRepository LocationRepository { get; }
        public IPostRepository PostRepository { get; }
        public IUserRepository UserRepository { get; }
        public IEmailRepository EmailRepository { get; }
        public IFilePostRepository FilePostRepository { get; }
    }
}
