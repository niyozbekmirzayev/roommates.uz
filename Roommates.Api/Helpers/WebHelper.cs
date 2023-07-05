using Microsoft.AspNetCore.Mvc;

namespace Roommates.Api.Helpers
{
    public static class WebHelper
    {
        public const string CLAIM_USER_ID = "UserId";

        public static IActionResult SentResponseWithStatusCode(Controller controller, dynamic response)
        {
            if (response.Error != null)
            {
                var errorResponse = new
                {
                    Data = (string)null,
                    response.Error,
                    response.ResponseCode
                };

                if (response.Error.Code == 404) return controller.NotFound(errorResponse);
                else if (response.Error.Code == 400) return controller.BadRequest(errorResponse);
                else if (response.Error.Code == 409) return controller.Conflict(errorResponse);
                else if (response.Error.Code == 401) return controller.Unauthorized(errorResponse);

                else return controller.StatusCode(500, errorResponse);
            }

            return controller.Ok(response);
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
