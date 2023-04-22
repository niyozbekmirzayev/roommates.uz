
namespace Roommates.GlobalHelper
{
    public static class Extensions : APplic
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<GlobalErrorHandlerMiddleware>();
        }
    }
}
