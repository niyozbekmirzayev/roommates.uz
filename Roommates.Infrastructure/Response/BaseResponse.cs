using Microsoft.Extensions.Logging.Abstractions;

namespace Roommates.Infrastructure.Response
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public string ResponseCode { get; set; }
        public BaseError Error { get; set; }
    }
}
