using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Roommates.Data.IRepositories;
using Roommates.Domain.Models.Roommates;
using Roommates.Service.Extensions;
using Roommates.Service.Helpers;
using Roommates.Service.Interfaces;
using Roommates.Service.Response;
using Roommates.Service.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Roommates.Service.Services
{
    public class IdentityService : IIdentiyService
    {
        private const string VERIFY_EMAIL_SUBJECT = "Verify your email address";

        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<IdentityService> logger;

        public IdentityService(
            IUserRepository userRepository, 
            IMapper mapper,
            IEmailService emailService,
            ILogger<IdentityService> logger,
            IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.emailService = emailService;
            this.configuration = configuration;
            this.logger = logger; 
        }

        public async Task<BaseResponse> CreateTokenAsync(CreateTokenViewModel createTokenView)
        {
            var response = new BaseResponse();
            try 
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(u => u.Email.ToLower() == createTokenView.Email.ToLower() &&
                                                      u.Password == createTokenView.Password.ToSHA256());

                if(user == null) 
                {
                    response.Error = new BaseError("invalid email or password", code: ErrorCodes.NotFoud);
                    response.ResponseCode = ResponseCodes.ERROR_NOT_FOUND_DATA;

                    return response;
                }

                // Need to validate for email confirmation
                var claims = new List<Claim>() 
                {
                     new Claim("UserId", user.Id.ToString()),
                     new Claim(ClaimTypes.Email, user.Email),
                     new Claim("FirstName", user.FirstName),
                     new Claim("LastName", user.LastName ?? string.Empty),
                     new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ??  string.Empty),
                };

                var issuer = configuration.GetSection("JWT:Issuer").Value;
                var audience = configuration.GetSection("JWT:Audience").Value;
                var key = configuration.GetSection("JWT:Key").Value;
                var expireTime = configuration.GetSection("JWT:Expire").Value;

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new JwtSecurityToken(issuer, audience, claims,
                                                           expires: DateTime.Now.AddMinutes(double.Parse(expireTime)),
                                                           signingCredentials: credentials);

                string token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

                response.Data = token;
                response.ResponseCode = ResponseCodes.SUCCESS_GENERATE_TOKEN;

                return response;
            }
            catch(Exception ex) 
            {
                return BaseHelperService.GetExceptionDetails(ex, logger);
            }
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
                user.Password = user.Password.ToSHA256();

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
                return BaseHelperService.GetExceptionDetails(ex, logger);
            }
        }

        public Task<BaseResponse> DeleteUserAsync(string password)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> UpdatePasswordAsync(UpdatePasswordViewModel updatePasswordView)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> VerifyEmail(VerifyEmailViewModel verifyEmailView)
        {
            throw new NotImplementedException();
        }

        private string CreateVerificationEmailBody(string verificationCode) 
        {
            return string.Empty;
        }
    }
}
