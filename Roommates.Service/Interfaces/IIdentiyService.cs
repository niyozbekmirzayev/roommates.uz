using Roommates.Service.Response;
using Roommates.Service.ViewModels;

namespace Roommates.Service.Interfaces
{
    public interface IIdentiyService
    {
        Task<BaseResponse> CreateTokenAsync(CreateTokenViewModel createTokenView);
        Task<BaseResponse> CreateUserAsync(CreateUserViewModel createUserView);
        Task<BaseResponse> DeleteUserAsync(string password);
        Task<BaseResponse> UpdatePasswordAsync(UpdatePasswordViewModel updatePasswordView);
        Task<BaseResponse> VerifyEmail(VerifyEmailViewModel verifyEmailView);
    }
}
