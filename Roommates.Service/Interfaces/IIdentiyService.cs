using Roommates.Global.Response;
using Roommates.Service.ViewModels;

namespace Roommates.Service.Interfaces
{
    public interface IIdentiyService
    {
        Task<BaseResponse> CreateTokenAsync(CreateTokenViewModel createTokenView);
        Task<BaseResponse> CreateUserAsync(CreateUserViewModel createUserView);
        Task<BaseResponse> CreateUserRemovalEmailAsync(string password);
        Task<BaseResponse> UpdatePasswordAsync(UpdatePasswordViewModel updatePasswordView);
        Task<BaseResponse> VerifyEmailAsync(string verificationCode);
        Task<BaseResponse> VerifyUserRemovalAsync(string verificationCode);
        Task<BaseResponse> CreatePasswordRecoveryEmailAsync(string emailAddress);
        Task<BaseResponse> RecoverPasswordAsync(RecoverPasswordViewModel recoverPasswordView);
    }
}
