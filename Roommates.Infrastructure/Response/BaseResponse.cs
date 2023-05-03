namespace Roommates.Infrastructure.Response
{
    public class BaseResponse
    {
        public object Data { get; set; } = null;
        public string ResponseCode { get; set; }
        public BaseError Error { get; set; }
    }
}
