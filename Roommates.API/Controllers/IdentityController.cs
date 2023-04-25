using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Helpers;
using Roommates.Service.Interfaces;
using Roommates.Service.ViewModels;

namespace Roommates.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
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
            var result = await identiyService.CreateUserAsync(createUserView);
            return WebHelper.SentResponseWithStatusCode(this, result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(CreateTokenViewModel createTokenView)
        {
            var result = await identiyService.CreateTokenAsync(createTokenView);
            return WebHelper.SentResponseWithStatusCode(this, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(string verifactionCode)
        {
            var result = await identiyService.VerifyEmailAsync(verifactionCode);
            return WebHelper.SentResponseWithStatusCode(this, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserRemovalEmail(string password)
        {
            var result = await identiyService.CreateUserRemovalEmailAsync(password);
            return WebHelper.SentResponseWithStatusCode(this, result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel updatePasswordViewModel)
        {
            var result = await identiyService.UpdatePasswordAsync(updatePasswordViewModel);
            return WebHelper.SentResponseWithStatusCode(this, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyUserRemovalEmail(string verifactionCode)
        {
            var result = await identiyService.VerifyUserRemovalAsync(verifactionCode);
            return WebHelper.SentResponseWithStatusCode(this, result);
        }
    }
}
