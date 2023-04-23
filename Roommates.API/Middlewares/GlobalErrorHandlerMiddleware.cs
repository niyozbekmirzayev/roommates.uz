using Newtonsoft.Json;
using Roommates.Global.Response;

namespace Roommates.API.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public GlobalErrorHandlerMiddleware(RequestDelegate next, ILogger logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, logger, context);
            }
        }

        public Task HandleExceptionAsync(Exception ex, ILogger logger, HttpContext context)
        {
            var response = new BaseResponse();

            response.ResponseCode = ResponseCodes.UNEXPECTED_ERROR_EXCEPTION;

            if (!string.IsNullOrEmpty(ex.Message))
            {
                logger.LogError(ex.Message, ex);

                response.Error = new BaseError(ex.Message);
            }
            else if (ex.InnerException != null)
            {
                logger.LogError(ex.InnerException.Message, ex.InnerException);

                response.Error = new BaseError(ex.InnerException.Message);
            }
            else
            {
                logger.LogError("Unexpected exception was thrown", ex);
            }

            string baseResponseJson = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            return context.Response.WriteAsync(baseResponseJson);
        }
    }
}
