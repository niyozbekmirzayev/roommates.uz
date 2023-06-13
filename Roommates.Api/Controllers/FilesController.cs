using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roommates.Api.Helpers;
using Roommates.Api.Interfaces;

namespace Roommates.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FilesController : Controller
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            this._fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            return WebHelper.SentResponseWithStatusCode(this, await _fileService.UploadFile(file));
        }
    }
}
