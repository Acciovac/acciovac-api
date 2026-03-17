using acciovac.API.Common;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace acciovac.API.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler 
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is ValidationException validationException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                    .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

                await httpContext.Response.WriteAsJsonAsync(ApiResponse.Failure("Validation error", new { errors }), cancellationToken);
                return true;
            }

            return false;
        }
    }  
}
