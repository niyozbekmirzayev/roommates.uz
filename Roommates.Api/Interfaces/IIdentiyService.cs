using Roommates.Api.Interfaces.Base;
using Roommates.Api.ViewModels;
using Roommates.Infrastructure.Response;

namespace Roommates.Api.Interfaces
{
    public interface IIdentiyService : IBaseService
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
