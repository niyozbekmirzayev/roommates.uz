using Microsoft.AspNetCore.Mvc;

namespace Roommates.Api.Helpers
{
    public static class WebHelper
    {
        public const string CLAIM_USER_ID = "UserId";

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

        public static Guid GetUserId(HttpContext httpContext)
        {
            var claims = httpContext.User.Claims.ToList();
            var userIdClaim = claims.FirstOrDefault(l => l.Type == CLAIM_USER_ID);

            if (userIdClaim == null)
            {
                throw new Exception("claim UserId not found");
            }

            Guid userId = Guid.Parse(userIdClaim.Value);

            return userId;
        }
    }
}
