﻿namespace Roommates.Infrastructure.Response
{
    public static class ResponseCodes
    {
        public const string SUCCESS_VERIFIED_DATA = "success-verified-data";
        public const string SUCCESS_ADD_DATA = "success-add-data";
        public const string SUCCESS_DELETE_DATA = "success-delete-data";
        public const string SUCCESS_GENERATE_TOKEN = "success-generate-token";
        public const string SUCCESS_UPDATE_DATA = "success-update-data";
        public const string SUCCESS_GET_DATA = "success-get-data";

        public const string ERROR_UNEXPECTED_EXCEPTION = "unexpected-error";
        public const string ERROR_SAVE_DATA = "error-save-data";
        public const string ERROR_DUPLICATE_DATA = "error-duplicate-data";
        public const string ERROR_NOT_FOUND_DATA = "error-not-found-data";
        public const string ERROR_TIMED_OUT_DATA = "error-time-out-data";
        public const string ERROR_NOT_VERIFIED = "error-not-verifed";
        public const string ERROR_INVALID_DATA = "error-invalid-data";
        public const string ERROR_INVALID_TOKEN = "error-invalid-token";
    }
}
