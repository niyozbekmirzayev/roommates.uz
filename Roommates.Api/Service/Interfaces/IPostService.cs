using Roommates.Api.Service.ViewModels;
using Roommates.Infrastructure.Response;

namespace Roommates.Api.Service.Interfaces
{
    public interface IPostService : IBaseService
    {
        Task<BaseResponse> CreatePostAsync(CreatePostViewModel viewModel);
        Task<BaseResponse> LikePostAsync(Guid postId);
        Task<BaseResponse> ViewPostAsync(Guid postId);
    }
}
