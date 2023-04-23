namespace Roommates.Global.Response
{
    public class BaseResponse
    {
        public object Data { get; set; }
        public string ResponseCode { get; set; }
        public BaseError Error { get; set; }
    }
}
