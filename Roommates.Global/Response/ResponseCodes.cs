namespace Roommates.Global.Response
{
    public static class ResponseCodes
    {
        public const string SUCCESS_VERIFIED_DATA = "success-verified-data";
        public const string SUCCESS_ADD_DATA = "success-add-data";
        public const string SUCCESS_DELETE_DATA = "success-delete-data";
        public const string SUCCESS_GENERATE_TOKEN = "success-generate-token";

        public const string UNEXPECTED_ERROR_EXCEPTION = "unexpected-error-exception";
        public const string ERROR_SAVE_DATA = "error-save-data";
        public const string ERROR_DUPLICATE_DATA = "error-duplicate-data";
        public const string ERROR_NOT_FOUND_DATA = "error-not-found-data";
        public const string ERROR_TIMED_OUT_DATA = "error-timed-out-data";
        public const string ERROR_NOT_VERIFIED = "error-not-verifed";
        public const string ERROR_INVALID_DATA = "error-invalid-data";
    }
}
