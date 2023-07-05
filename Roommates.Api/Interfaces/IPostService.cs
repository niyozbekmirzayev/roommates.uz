using Roommates.Api.Interfaces.Base;
using Roommates.Api.ViewModels;
using Roommates.Infrastructure.Response;

namespace Roommates.Api.Interfaces
{
    public interface IPostService : IBaseService
    {
        Task<BaseResponse<Guid>> CreatePostAsync(CreatePostViewModel viewModel);
        Task<BaseResponse<Guid>> LikePostAsync(Guid postId);

        Task<BaseResponse<Guid>> DislikePostAsync(Guid postId);
        Task<BaseResponse<ViewPostViewModel>> GetPostAsync(Guid postId);

        Task<BaseResponse<IEnumerable<ViewPostViewModel>>> GetPostsAsync(int skip, int take);

        Task<BaseResponse<IEnumerable<ViewPostViewModel>>> GetLikedPostsAsync(int skip, int take);
    }
}
