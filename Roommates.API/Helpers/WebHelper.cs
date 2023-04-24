using Microsoft.AspNetCore.Mvc;

namespace Roommates.API.Helpers
{
    public static class WebHelper
    {
        public static IActionResult SentResponseWithStatusCode(Controller controller, dynamic source)
        {
            if (source.Error != null)
            {
                if (source.Error.Code == 404) return controller.NotFound(source);
                else if (source.Error.Code == 400) return controller.BadRequest(source);
                else if (source.Error.Code == 409) return controller.Conflict(source);
                else if (source.Error.Code == 401) return controller.Unauthorized(source);

                else return controller.StatusCode(500, source);
            }

            return controller.Ok(source);
        }
    }
}
