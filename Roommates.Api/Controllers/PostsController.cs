using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roommates.Api.Helpers;
using Roommates.Api.Interfaces;
using Roommates.Api.ViewModels;

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

        [HttpGet]
        public async Task<IActionResult> ViewPost(Guid postId)
        {
            return WebHelper.SentResponseWithStatusCode(this, await postService.ViewPostAsync(postId));
        }

        [HttpPost]
        public async Task<IActionResult> LikePost(Guid postId)
        {
            return WebHelper.SentResponseWithStatusCode(this, await postService.LikePostAsync(postId));
        }

        [HttpPost]
        public async Task<IActionResult> DislikePost(Guid postId)
        {
            return WebHelper.SentResponseWithStatusCode(this, await postService.DislikePostAsync(postId));
        }
    }
}
