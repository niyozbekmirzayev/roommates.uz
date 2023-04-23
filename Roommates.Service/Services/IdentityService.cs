using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Roommates.Data.IRepositories;
using Roommates.Domain.Enums;
using Roommates.Domain.Models.Roommates;
using Roommates.Domain.Models.Users;
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
        private readonly IUnitOfWorkRepository unitOfWorkRepository;

        public IdentityService(
            IUserRepository userRepository,
            IMapper mapper,
            IEmailService emailService,
            ILogger<IdentityService> logger,
            IUnitOfWorkRepository unitOfWorkRepository,
            IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.emailService = emailService;
            this.configuration = configuration;
            this.logger = logger;
            this.unitOfWorkRepository = unitOfWorkRepository;
        }

        public async Task<BaseResponse> CreateTokenAsync(CreateTokenViewModel createTokenView)
        {
            var response = new BaseResponse();

            var user = await userRepository.GetAll().FirstOrDefaultAsync(u => u.Email.ToLower() == createTokenView.Email.ToLower() &&
                                                    u.Password == createTokenView.Password.ToSHA256());

            if (user == null)
            {
                response.Error = new BaseError("invalid email or password", code: ErrorCodes.NotFoud);
                response.ResponseCode = ResponseCodes.ERROR_NOT_FOUND_DATA;

                return response;
            }

            if(user.EmailVerifiedDate == null) 
            {
                response.Error = new BaseError("email is not verified", code: ErrorCodes.Unauthorized);
                response.ResponseCode = ResponseCodes.ERROR_NOT_VERIFIED;

                return response;
            }
            
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

        public async Task<BaseResponse> CreateUserAsync(CreateUserViewModel createUserView)
        {
            var response = new BaseResponse();
            bool isEmailDuplicated = userRepository.GetAll().Any(l => l.Email == createUserView.Email);
            if (isEmailDuplicated)
            {
                response.Error = new BaseError("email is duplicated", code: ErrorCodes.Conflict);
                response.ResponseCode = ResponseCodes.ERROR_DUPLICATE_DATA;

                return response;
            }

            var user = mapper.Map<User>(createUserView);
            user.Password = user.Password.ToSHA256();

            user.EmailVerifications = new List<EmailVerification>
            {
                new EmailVerification()
                {
                    Type = EmailVerificationType.EmailConfirmation,
                }
            };
            var createdUser = await userRepository.AddAsync(user);

            var verificationEmailBody = CreateEmailVerificationBody(user.EmailVerifications.FirstOrDefault(), user);
            var emailView = new EmailViewModel
            {
                To = user.Email,
                Subject = VERIFY_EMAIL_SUBJECT,
                Body = verificationEmailBody,
            };

            await emailService.SendEmailAsync(emailView);

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

        public Task<BaseResponse> DeleteUserAsync(string password)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> UpdatePasswordAsync(UpdatePasswordViewModel updatePasswordView)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> VerifyEmailAsync(string verificationCode)
        {
            var response = new BaseResponse();
            var emailVerification = await unitOfWorkRepository.EmailVerificationRepository.GetAll().Include(l => l.User)
                                .FirstOrDefaultAsync(s => s.VerificationCode == verificationCode && 
                                                          s.Type == EmailVerificationType.EmailConfirmation);

            if(emailVerification == null) 
            {
                response.Error = new BaseError("verification code not found", code: ErrorCodes.NotFoud);
                response.ResponseCode = ResponseCodes.ERROR_NOT_FOUND_DATA;

                return response;
            }

            if (emailVerification.ExpirationDate < DateTime.UtcNow) 
            {
                response.Error = new BaseError("verifcatin code timed out", code: ErrorCodes.Gone);
                response.ResponseCode = ResponseCodes.ERROR_TIMED_OUT_DATA;

                return response;
            }

            if(emailVerification.User.EmailVerifiedDate != null && emailVerification.VerifiedDate != null) 
            {
                response.ResponseCode = ResponseCodes.SUCCESS_VERIFIED_DATA;

                return response;
            }

            emailVerification.VerifiedDate = DateTime.UtcNow;
            emailVerification.User.EmailVerifiedDate = emailVerification.VerifiedDate;
            unitOfWorkRepository.EmailVerificationRepository.Update(emailVerification);

            if (await userRepository.SaveChangesAsync() > 0)
            {
                response.ResponseCode = ResponseCodes.SUCCESS_VERIFIED_DATA;

                return response;
            }

            response.Error = new BaseError("no changes made in the database");
            response.ResponseCode = ResponseCodes.ERROR_SAVE_DATA;

            return response;
        }

        private string CreateEmailVerificationBody(EmailVerification emailVerification, User user)
        {
            string confirmationLink = $"https://localhost:7078/api/Identity/VerifyEmail?verifactionCode={emailVerification.VerificationCode}";

            var htmlEmailBody = $"<!DOCTYPE html>" +
                $"<html>" +
                $" <head>" +
                $"    <title>Email Confirmation</title>" +
                $"  </head>" +
                $"  <body>" +
                $"    <h1>Email Confirmation</h1>" +
                $"    <p>Dear {user.FirstName},</p>" +
                $"    <p>Thank you for registering. Please click on the following link to confirm your email address:</p>" +
                $"    <p><a href=\"{confirmationLink}\"> Confimation link </a></ p >" +
                $"    <p>If you did not initiate this request, please ignore this email.</p>" +
                $"    <p>Thank you for choosing our service!</p>" +
                $"    <p>Best regards</p>" +
                $"  </body>" +
                $"</html>";

            return htmlEmailBody;
        }
    }
}