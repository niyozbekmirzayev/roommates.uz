using Microsoft.AspNetCore.Mvc;

namespace Roommates.API.Helpers
{
    public class WebFunctions : Controller 
    {
        public IActionResult SentResponseWithStatusCode(dynamic source)
        {
            if (source.Error != null)
            {
                if (source.Error.Code == 404) return NotFound(source);
                else if (source.Error.Code == 400) return BadRequest(source);
                else if (source.Error.Code == 409) return Conflict(source);
                else if (source.Error.Code == 401) return Unauthorized(source);
                else if (source.Error.Code == 500) return StatusCode(500, source);
                else return StatusCode(500, source);
            }

            return Ok(source);
        }
    }
}
