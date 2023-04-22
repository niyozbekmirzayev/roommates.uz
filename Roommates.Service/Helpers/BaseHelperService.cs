using Roommates.Service.Response;

namespace Roommates.Service.Helpers
{
    public static class BaseHelperService
    {
        public static BaseResponse GetExceptionDetails(Exception ex) 
        {
            var response = new BaseResponse();

            response.ResponseCode = ResponseCodes.UNEXPECTED_ERROR_EXCEPTION;

            if (!string.IsNullOrEmpty(ex.Message)) 
            {
                response.Error = new BaseError(ex.Message);
            }
            else if(ex.InnerException != null) 
            {
                response.Error = new BaseError(ex.InnerException.Message);
            }
            else 
            {
                return response;
            }

            return response;
        }
    }
}
