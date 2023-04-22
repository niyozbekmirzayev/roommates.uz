using Microsoft.Extensions.Logging;
using Roommates.Service.Response;

namespace Roommates.Service.Helpers
{
    public static class BaseHelperService
    {
        public static BaseResponse GetExceptionDetails(Exception ex, ILogger logger)
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

                return response;
            }

            return response;
        }
    }
}
