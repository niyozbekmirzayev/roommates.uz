namespace Roommates.Service.Response
{
    public class BaseError
    {
        public BaseError(string message, object exception = null, int code = 500)
        {
            Code = code;
            Message = message;
            Exception = exception;
        }

        public object Exception { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }
}
