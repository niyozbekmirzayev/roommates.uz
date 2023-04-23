using Microsoft.AspNetCore.Http;

namespace Roommates.Global.Helpers
{
    public static class WebHelper
    {
        public static Guid GetUserId(HttpContext httpContext)
        {
            return Guid.NewGuid();
        }
    }
}
