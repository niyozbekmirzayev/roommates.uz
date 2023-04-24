using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Roommates.Data.IRepositories;
using Roommates.Data.Repositories;
using Roommates.Domain.Enums;
using Roommates.Domain.Models.Roommates;
using Roommates.Domain.Models.Users;
using Roommates.Global.Helpers;
using Roommates.Global.Response;
using Roommates.Service.Interfaces;
using Roommates.Service.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Roommates.Service.Services
{
    public class IdentityService : IIdentiyService
    {
        private const string EMAIL_SUBJECT_VERIFY_EMAIL = "Verify your email address";
        private const string EMAIL_SUBJECT_USER_REMOVAL = "Verify Account Removal";

        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<IdentityService> logger;
        private readonly IUnitOfWorkRepository unitOfWorkRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public IdentityService(
            IUserRepository userRepository,
            IMapper mapper,
            IEmailService emailService,
            ILogger<IdentityService> logger,
            IUnitOfWorkRepository unitOfWorkRepository,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.emailService = emailService;
            this.configuration = configuration;
            this.logger = logger;
            this.unitOfWorkRepository = unitOfWorkRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse> CreateTokenAsync(CreateTokenViewModel createTokenView)
        {
            var response = new BaseResponse();

            var user = await userRepository.GetAll().FirstOrDefaultAsync(u => u.EmailAddress.ToLower() == createTokenView.Email.ToLower() &&
                                                    u.Password == createTokenView.Password.ToSHA256());

            if (user == null)
            {
                response.Error = new BaseError("invalid email or password", code: ErrorCodes.NotFoud);
                response.ResponseCode = ResponseCodes.ERROR_NOT_FOUND_DATA;

                return response;
            }

            if (user.EmailVerifiedDate == null)
            {
                response.Error = new BaseError("email is not verified", code: ErrorCodes.Unauthorized);
                response.ResponseCode = ResponseCodes.ERROR_NOT_VERIFIED;

                return response;
            }

            var claims = new List<Claim>()
            {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.EmailAddress),
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
            bool isEmailDuplicated = userRepository.GetAll().Any(l => l.EmailAddress == createUserView.EmailAddress);
            if (isEmailDuplicated)
            {
                response.Error = new BaseError("email is duplicated", code: ErrorCodes.Conflict);
                response.ResponseCode = ResponseCodes.ERROR_DUPLICATE_DATA;

                return response;
            }

            var user = mapper.Map<User>(createUserView);
            user.Password = user.Password.ToSHA256();

            var emailExpirationTime = int.Parse(configuration.GetSection("EmailExpirationTime").Value);
            user.EmailVerifications = new List<Email>
            {
                new Email()
                {
                    ExpirationDate = DateTime.UtcNow.AddHours(emailExpirationTime),
                    Type = EmailType.EmailVerification,
                    EmailAddress = user.EmailAddress,
                }
            };
            var createdUser = await userRepository.AddAsync(user);

            var verificationEmailBody = GetEmailVerificationEmailBody(user.EmailVerifications.FirstOrDefault(), user);
            var emailView = new EmailViewModel
            {
                To = user.EmailAddress,
                Subject = EMAIL_SUBJECT_VERIFY_EMAIL,
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

        public async Task<BaseResponse> CreateUserRemovalEmailAsync(string password)
        {
            var response = new BaseResponse();

            Guid currentUserId = WebFunctions.GetUserId(httpContextAccessor.HttpContext);

            var currentUser = await unitOfWorkRepository.UserRepository.Get(currentUserId);
            if (currentUser == null)
            {
                response.ResponseCode = ResponseCodes.ERROR_NOT_FOUND_DATA;
                response.Error = new BaseError("current user not found", ErrorCodes.NotFoud);

                return response;
            }

            password = password.ToSHA256();
            if (!currentUser.Password.Equals(password))
            {
                response.ResponseCode = ResponseCodes.ERROR_INVALID_DATA;
                response.Error = new BaseError("invalid password", ErrorCodes.BadRequest);

                return response;
            }

            var emailExpirationTime = int.Parse(configuration.GetSection("EmailExpirationTime").Value);
            var userRemovalEmail = new Email()
            {
                Type = EmailType.UserRemoval,
                UserId = currentUserId,
                ExpirationDate = DateTime.UtcNow.AddHours(emailExpirationTime),
                EmailAddress = currentUser.EmailAddress,
            };

            var createdVerification = await unitOfWorkRepository.EmailRepository.AddAsync(userRemovalEmail);

            var emailBody = GetUserRemovalEmailBody(createdVerification, currentUser);
            var emailView = new EmailViewModel
            {
                To = currentUser.EmailAddress,
                Subject = EMAIL_SUBJECT_USER_REMOVAL,
                Body = emailBody,
            };

            await emailService.SendEmailAsync(emailView);


            if (await unitOfWorkRepository.EmailRepository.SaveChangesAsync() > 0)
            {
                response.ResponseCode = ResponseCodes.SUCCESS_ADD_DATA;
                response.Data = createdVerification.Id;

                return response;
            }

            response.Error = new BaseError("no changes made in the database");
            response.ResponseCode = ResponseCodes.ERROR_SAVE_DATA;

            return response;
        }

        public Task<BaseResponse> UpdatePasswordAsync(UpdatePasswordViewModel updatePasswordView)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> VerifyEmailAsync(string verificationCode)
        {
            var response = new BaseResponse();
            var email = await unitOfWorkRepository.EmailRepository.GetAll().Include(l => l.User)
                                .FirstOrDefaultAsync(s => s.VerificationCode == verificationCode &&
                                                          s.Type == EmailType.EmailVerification);

            if (email == null)
            {
                response.Error = new BaseError("verification code not found", code: ErrorCodes.NotFoud);
                response.ResponseCode = ResponseCodes.ERROR_NOT_FOUND_DATA;

                return response;
            }

            if (email.ExpirationDate < DateTime.UtcNow)
            {
                response.Error = new BaseError("email timed out", code: ErrorCodes.Gone);
                response.ResponseCode = ResponseCodes.ERROR_TIMED_OUT_DATA;

                return response;
            }

            if (email.User.EmailVerifiedDate != null && email.VerifiedDate != null)
            {
                response.ResponseCode = ResponseCodes.SUCCESS_VERIFIED_DATA;

                return response;
            }

            email.VerifiedDate = DateTime.UtcNow;
            email.User.EmailVerifiedDate = email.VerifiedDate;
            unitOfWorkRepository.EmailRepository.Update(email);

            if (await userRepository.SaveChangesAsync() > 0)
            {
                response.ResponseCode = ResponseCodes.SUCCESS_VERIFIED_DATA;

                return response;
            }

            response.Error = new BaseError("no changes made in the database");
            response.ResponseCode = ResponseCodes.ERROR_SAVE_DATA;

            return response;
        }

        public async Task<BaseResponse> VerifyUserRemovalAsync(string verificationCode) 
        {
            var response = new BaseResponse();

            var email = await unitOfWorkRepository.EmailRepository.GetAll().Include(l => l.User)
                .FirstOrDefaultAsync(l => l.VerificationCode == verificationCode && l.Type == EmailType.UserRemoval);

            if (email == null) 
            {
                response.Error = new BaseError("verification code not found", code: ErrorCodes.NotFoud);
                response.ResponseCode = ResponseCodes.ERROR_NOT_FOUND_DATA;

                return response;
            }

            if (email.ExpirationDate < DateTime.UtcNow)
            {
                response.Error = new BaseError("email timed out", code: ErrorCodes.Gone);
                response.ResponseCode = ResponseCodes.ERROR_TIMED_OUT_DATA;

                return response;
            }

            if(email.User == null && email.VerifiedDate != null) 
            {
                response.ResponseCode = ResponseCodes.SUCCESS_VERIFIED_DATA;

                return response;
            }

            email.VerifiedDate = DateTime.UtcNow;

            userRepository.Remove(email.User);
            unitOfWorkRepository.EmailRepository.Update(email);

            if (await userRepository.SaveChangesAsync() > 0)
            {
                response.ResponseCode = ResponseCodes.SUCCESS_VERIFIED_DATA;

                return response;
            }

            response.Error = new BaseError("no changes made in the database");
            response.ResponseCode = ResponseCodes.ERROR_SAVE_DATA;

            return response;
        }

        private string GetEmailVerificationEmailBody(Email emailVerification, User user)
        {
            string localIpAddress = httpContextAccessor.HttpContext.Request.Host.Value.ToString();
            string confirmationLink = $"https://{localIpAddress}/api/Identity/VerifyEmail?verifactionCode={emailVerification.VerificationCode}";

            var htmlEmailBody = $"<!DOCTYPE html>" +
                $"<html>" +
                $" <head>" +
                $"    <title>Email Confirmation</title>" +
                $"  </head>" +
                $"  <body>" +
                $"    <h1>Email Confirmation</h1>" +
                $"    <p>Dear {user.FirstName},</p>" +
                $"    <p>Thank you for registering. To confirm your email address, please click the button below.</p>" +
                $"   <a href=\"{confirmationLink}\" target=\"_blank\" style=\"display:inline-block;padding:12px 24px;background-color:#3366cc;color:#ffffff;font-size:18px;text-decoration:none;border-radius:5px;\">Confirm Email</a>" +
                $"    <p>If you did not initiate this request, please ignore this email.</p>" +
                $"    <p>Thank you for choosing our service!</p>" +
                $"    <p>Best regards</p>" +
                $"  </body>" +
                $"</html>";

            return htmlEmailBody;
        }

        private string GetUserRemovalEmailBody(Email emailVerification, User user)
        {
            string localIpAddress = httpContextAccessor.HttpContext.Request.Host.Value.ToString();
            string confirmationLink = $"https://{localIpAddress}/api/Identity/VerifyUserRemovalEmail?verifactionCode={emailVerification.VerificationCode}";

            var htmlEmailBody = "<!DOCTYPE html>" +
                $"<html>" +
                $"  <head>" +
                $"    <meta charset=\"UTF-8\">" +
                $"    <title>Account Removal Verification</title>" +
                $"  </head>" +
                $"  <body>" +
                $"    <h1>Account Removal Verification</h1>" +
                $"   <p>Dear {user.FirstName},</p>" +
                $"    <p>We have received a request to remove your account. To confirm that you wish to proceed with this request, please click the button below.</p>" +
                $"    <p><strong>Note:</strong> If you did not request to remove your account, please ignore this email</p>" +
                $"    <a href=\"{confirmationLink}\" target=\"_blank\" style=\"display:inline-block;padding:12px 24px;background-color:#FF0000;color:#ffffff;font-size:18px;text-decoration:none;border-radius:5px;\">Verify Account Removal</a>" +
                $"    <p>Thank you for choosing our service!</p>" +
                $"    <p>Best regards</p>" +
                $"  </body>" +
                $"</html>";

            return htmlEmailBody;
        }
    }
}