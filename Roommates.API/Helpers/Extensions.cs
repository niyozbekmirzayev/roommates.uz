using Roommates.API.Middlewares;

namespace Roommates.API.Helpers
{
    public static class Extensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
        }
    }
}
