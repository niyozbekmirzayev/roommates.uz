using Roommates.Infrastructure.Response;

namespace Roommates.Global.Helpers
{
    public static class ServiceFunctions
    {
        public static BaseResponse GetExceptionDetails(Exception ex, Serilog.ILogger logger)
        {
            var response = new BaseResponse();

            response.ResponseCode = ResponseCodes.ERROR_UNEXPECTED_EXCEPTION;

            if (!string.IsNullOrEmpty(ex.Message))
            {
                logger.Error(ex.Message, ex);

                response.Error = new BaseError(ex.Message);
            }
            else if (ex.InnerException != null)
            {
                logger.Error(ex.InnerException.Message, ex.InnerException);

                response.Error = new BaseError(ex.InnerException.Message);
            }
            else
            {
                logger.Error("Unexpected exception was thrown", ex);

                return response;
            }

            return response;
        }
    }
}
