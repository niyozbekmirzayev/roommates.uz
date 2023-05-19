using Roommates.Infrastructure.Response;

namespace Roommates.Api.Service.Interfaces
{
    public interface IFileService : IBaseService
    {
        public Task<BaseResponse> UploadFile(IFormFile file);
    }
}
