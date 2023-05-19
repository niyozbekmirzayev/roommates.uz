using Roommates.Api.Service.ViewModels.PostService;
using Roommates.Infrastructure.Response;

namespace Roommates.Api.Service.Interfaces
{
    public interface IPostService : IBaseService
    {
        Task<BaseResponse> CreatePostAsync(CreatePostViewModel viewModel);
    }
}
