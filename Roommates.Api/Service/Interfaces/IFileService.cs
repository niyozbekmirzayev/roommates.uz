using Roommates.Infrastructure.Response;

namespace Roommates.Api.Service.Interfaces
{
    public interface IFileService : IBaseService
    {
        Task<BaseResponse> UploadFile(IFormFile file);
    }
}
