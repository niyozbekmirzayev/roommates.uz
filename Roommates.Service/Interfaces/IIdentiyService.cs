using Roommates.Service.Response;
using Roommates.Service.ViewModels;

namespace Roommates.Service.Interfaces
{
    public interface IIdentiyService
    {
        Task<BaseResponse> CreateTokenAsync(CreateTokenViewModel createTokenView);
        Task<BaseResponse> CreateUserAsync(CreateUserViewModel createUserView);
        Task<bool> DeleteUserAsync(string password);
        Task<bool> UpdatePasswordAsync(UpdatePasswordViewModel updatePasswordView);
        Task<bool> VerifyEmail(VerifyEmailViewModel verifyEmailView);
    }
}
