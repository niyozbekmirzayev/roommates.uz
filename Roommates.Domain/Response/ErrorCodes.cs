
namespace Roommates.Infrastructure.Response
{
    public static class ErrorCodes
    {
        public const int Conflict = 409;
        public const int BadRequest = 400;
        public const int Unauthorized = 401;
        public const int NotFoud = 404;
        public const int Gone = 410;
        public const int InvalidToken = 498;
    }
}
