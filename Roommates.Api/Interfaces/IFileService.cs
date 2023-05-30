using Roommates.Infrastructure.Response;

namespace Roommates.Api.Interfaces
{
    public interface IFileService : IBaseService
    {
        Task<BaseResponse> UploadFile(IFormFile file);
    }
}
