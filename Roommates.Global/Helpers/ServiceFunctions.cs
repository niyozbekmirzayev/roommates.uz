using Roommates.Global.Response;
using Serilog;

namespace Roommates.Global.Helpers
{
    public static class ServiceFunctions
    {
        public static BaseResponse GetExceptionDetails(Exception ex, ILogger logger)
        {
            var response = new BaseResponse();

            response.ResponseCode = ResponseCodes.UNEXPECTED_ERROR_EXCEPTION;

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
