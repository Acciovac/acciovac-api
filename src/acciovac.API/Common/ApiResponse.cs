namespace acciovac.API.Common
{
    public static class ApiResponse
    {
        public static object Success(object? result = null) => new
        {
            status = "success",
            error = (string?)null,
            result = result ?? new { }
        };

        public static object Failure(string? error, object? result = null) => new
        {
            status = "error",
            error,
            result = result ?? new { }
        };
    }
}
