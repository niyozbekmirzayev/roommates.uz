using AutoMapper;
using Roommates.Data.IRepositories;
using Roommates.Domain.Models.Roommates;
using Roommates.Service.Interfaces;
using Roommates.Service.Response;
using Roommates.Service.ViewModels;

namespace Roommates.Service.Services
{
    public class IdentityService : IIdentiyService
    {
        private const string VERIFY_EMAIL_SUBJECT = "Verify your email address";

        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;
        private readonly IMapper mapper;

        public IdentityService(IUserRepository userRepository, IMapper mapper, IEmailService emailService)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.emailService = emailService;
        }

        public Task<string> CreateTokenAsync(CreateTokenViewModel createTokenView)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> CreateUserAsync(CreateUserViewModel createUserView)
        {
            var response = new BaseResponse();
            try
            {
                bool isEmailDuplicated = userRepository.GetAll().Any(l => l.Email == createUserView.Email);
                if (isEmailDuplicated)
                {
                    response.Error = new BaseError("email is duplicated", code: ErrorCodes.Conflict);
                    response.ResponseCode = ResponseCodes.ERROR_DUPLICATE_DATA;

                    return response;
                }

                var user = mapper.Map<User>(createUserView);

                // Email verification
                /*var verificationEmailBody = CreateVerificationEmailBody(user.EmailVerificationCode);
                var emailView = new EmailViewModel
                {
                    To = user.Email,
                    Subject = VERIFY_EMAIL_SUBJECT,
                    Body = verificationEmailBody,
                };
                
                await emailService.SendEmailAsync(emailView);*/

                var createdUser = await userRepository.AddAsync(user);

                if (await userRepository.SaveChangesAsync() > 0)
                {
                    response.Data = createdUser.Id;
                    response.ResponseCode = ResponseCodes.SUCCESS_ADD_DATA;

                    return response;
                }

                response.Error = new BaseError("no changes made in the database");
                response.ResponseCode = ResponseCodes.ERROR_SAVE_DATA;

                return response;
            }
            catch (Exception ex)
            {
                response.ResponseCode = ResponseCodes.UNEXPECTED_ERROR;
                response.Error = new BaseError(ex.Message, ex);

                return response;
            }
        }

        public Task<bool> DeleteUserAsync(string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePasswordAsync(UpdatePasswordViewModel updatePasswordView)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyEmail(VerifyEmailViewModel verifyEmailView)
        {
            throw new NotImplementedException();
        }

        private string CreateVerificationEmailBody(string verificationCode) 
        {
            return string.Empty;
        }
    }
}
