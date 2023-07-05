using Roommates.Api.Interfaces.Base;
using Roommates.Api.ViewModels;
using Roommates.Infrastructure.Response;

namespace Roommates.Api.Interfaces
{
    public interface IIdentiyService : IBaseService
    {
        Task<BaseResponse<GeneratedTokenViewModel>> CreateTokenAsync(CreateTokenViewModel createTokenView);
        Task<BaseResponse<Guid>> CreateUserAsync(CreateUserViewModel createUserView);
        Task<BaseResponse<Guid>> CreateUserRemovalEmailAsync(string password);
        Task<BaseResponse<bool>> UpdatePasswordAsync(UpdatePasswordViewModel updatePasswordView);
        Task<BaseResponse<bool>> VerifyEmailAsync(string verificationCode);
        Task<BaseResponse<bool>> VerifyUserRemovalAsync(string verificationCode);
        Task<BaseResponse<Guid>> CreatePasswordRecoveryEmailAsync(string emailAddress);
        Task<BaseResponse<bool>> RecoverPasswordAsync(RecoverPasswordViewModel recoverPasswordView);
    }
}
