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
        private readonly WebFunctions webFunctions;

        public IdentityController(IIdentiyService identiyService)
        {
            this.identiyService = identiyService;
            webFunctions = new WebFunctions();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(CreateUserViewModel createUserView)
        {
            var result = await identiyService.CreateUserAsync(createUserView);
            return webFunctions.SentResponseWithStatusCode(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(CreateTokenViewModel createTokenView)
        {
            var result = await identiyService.CreateTokenAsync(createTokenView);
            return webFunctions.SentResponseWithStatusCode(result);
        }
    }
}
