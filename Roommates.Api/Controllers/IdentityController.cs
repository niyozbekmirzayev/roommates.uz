using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roommates.Api.Helpers;
using Roommates.Api.Service.Interfaces;
using Roommates.Api.Service.ViewModels;

namespace Roommates.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class IdentityController : Controller
    {
        private readonly IIdentiyService identiyService;

        public IdentityController(IIdentiyService identiyService)
        {
            this.identiyService = identiyService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(CreateUserViewModel createUserView)
        {
           return WebHelper.SentResponseWithStatusCode(this, await identiyService.CreateUserAsync(createUserView));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(CreateTokenViewModel createTokenView)
        {
            return WebHelper.SentResponseWithStatusCode(this, await identiyService.CreateTokenAsync(createTokenView));
        }

        [HttpPatch]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(string verifactionCode)
        {
            return WebHelper.SentResponseWithStatusCode(this, await identiyService.VerifyEmailAsync(verifactionCode));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserRemovalEmail(string password)
        {
            return WebHelper.SentResponseWithStatusCode(this, await identiyService.CreateUserRemovalEmailAsync(password));
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel updatePasswordView)
        {
            return WebHelper.SentResponseWithStatusCode(this, await identiyService.UpdatePasswordAsync(updatePasswordView));
        }

        [HttpPatch]
        [AllowAnonymous]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel recoverPasswordView)
        {
            return WebHelper.SentResponseWithStatusCode(this, await identiyService.RecoverPasswordAsync(recoverPasswordView));
        }

        [HttpPatch]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyUserRemovalEmail(string verifactionCode)
        {
            return WebHelper.SentResponseWithStatusCode(this, await identiyService.VerifyUserRemovalAsync(verifactionCode));
        }
    }
}
