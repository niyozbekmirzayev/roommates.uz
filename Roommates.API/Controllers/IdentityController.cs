using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roommates.API.Helpers;
using Roommates.Service.Interfaces;
using Roommates.Service.ViewModels;

namespace Roommates.API.Controllers
{
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
            var result = await identiyService.CreateUserAsync(createUserView);
            return WebFunctions.SentResponseWithStatusCode(this, result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(CreateTokenViewModel createTokenView)
        {
            var result = await identiyService.CreateTokenAsync(createTokenView);
            return WebFunctions.SentResponseWithStatusCode(this, result);
        }
    }
}
