using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roommates.Api.Helpers;
using Roommates.Api.Service.Interfaces;
using Roommates.Api.Service.ViewModels.PostService;

namespace Roommates.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PostsController : Controller
    {
        private IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostViewModel viewModel)
        {
            return WebHelper.SentResponseWithStatusCode(this, await postService.CreatePostAsync(viewModel));
        }
    }
}
