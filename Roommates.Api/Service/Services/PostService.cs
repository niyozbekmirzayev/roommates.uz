using AutoMapper;
using Roommates.Api.Data.IRepositories;
using Roommates.Api.Helpers;
using Roommates.Api.Service.Interfaces;
using Roommates.Api.Service.ViewModels.PostService;
using Roommates.Infrastructure.Enums;
using Roommates.Infrastructure.Models;
using Roommates.Infrastructure.Response;

namespace Roommates.Api.Service.Services
{
    public class PostService : BaseService, IPostService
    {
        private readonly IPostRepository postRepository;

        public PostService(
           IPostRepository postRepository,
           IMapper mapper,
           ILogger<PostService> logger,
           IUnitOfWorkRepository unitOfWorkRepository,
           IHttpContextAccessor httpContextAccessor,
           IConfiguration configuration) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
            this.postRepository = postRepository;
        }

        public async Task<BaseResponse> CreatePostAsync(CreatePostViewModel viewModel)
        {
            var response = new BaseResponse();

            var currentUserId = WebHelper.GetUserId(httpContextAccessor.HttpContext);

            var newLocation = mapper.Map<Location>(viewModel.Location);
            newLocation.AuthorUserId = currentUserId;
            newLocation = await unitOfWorkRepository.LocationRepository.AddAsync(newLocation);

            var newPost = mapper.Map<Post>(viewModel);
            newPost.CreatedByUserId = currentUserId;
            newPost.LocationId = newLocation.Id;

            newPost = await postRepository.AddAsync(newPost);

            // Saving AppartmentViewFiles
            if (viewModel.AppartmentViewFiles != null && viewModel.AppartmentViewFiles.Any(l => l.ActionType == ActionType.Create))
            {
                var filesIds = viewModel.AppartmentViewFiles.Where(l => l.ActionType == ActionType.Create).Select(l => l.FileId);
                var files = unitOfWorkRepository.FileRepository.GetAll().Where(l => filesIds.Contains(l.Id));

                foreach (var file in files)
                {
                    var newFilePost = new FilePost()
                    {
                        FileId = file.Id,
                        PostId = newPost.Id,
                    };

                    await unitOfWorkRepository.FilePostRepository.AddAsync(newFilePost);
                }
            }

            if (await postRepository.SaveChangesAsync() > 0)
            {
                response.Data = newPost.Id;
                response.ResponseCode = ResponseCodes.SUCCESS_ADD_DATA;

                return response;
            }

            response.Error = new BaseError("no changes made in the database");
            response.ResponseCode = ResponseCodes.ERROR_SAVE_DATA;

            return response;
        }
    }
}
