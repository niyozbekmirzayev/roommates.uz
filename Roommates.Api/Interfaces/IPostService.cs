using Roommates.Api.Interfaces.Base;
using Roommates.Api.ViewModels;
using Roommates.Infrastructure.Response;

namespace Roommates.Api.Interfaces
{
    public interface IPostService : IBaseService
    {
        Task<BaseResponse> CreatePostAsync(CreatePostViewModel viewModel);
        Task<BaseResponse> LikePostAsync(Guid postId);

        Task<BaseResponse> DislikePostAsync(Guid postId);
        Task<BaseResponse> ViewPostAsync(Guid postId);
    }
}
