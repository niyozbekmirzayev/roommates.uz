using AutoMapper;
using Roommates.Api.Data.IRepositories;
using Roommates.Api.Service.Interfaces;
using Roommates.Api.Service.ViewModels.PostService;
using Roommates.Infrastructure.Response;

namespace Roommates.Api.Service.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<PostService> logger;
        private readonly IUnitOfWorkRepository unitOfWorkRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PostService(
           IPostRepository postRepository,
           IMapper mapper,
           ILogger<PostService> logger,
           IUnitOfWorkRepository unitOfWorkRepository,
           IHttpContextAccessor httpContextAccessor,
           IConfiguration configuration)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
            this.unitOfWorkRepository = unitOfWorkRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<BaseResponse> CreatePostAsync(CreatePostViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
