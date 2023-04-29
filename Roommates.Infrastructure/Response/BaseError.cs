namespace Roommates.Infrastructure.Response
{
    public class BaseError
    {
        public BaseError(string message, int code = 500)
        {
            Code = code;
            Message = message;
        }

        public string Message { get; set; }
        public int Code { get; set; }
    }
}
