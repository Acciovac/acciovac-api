using acciovac.Application.Abstractions;
using System.Security.Claims;

namespace acciovac.API.Middleware
{
    public class FirebaseAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IFirebaseAuthService _firebaseAuthService;

        public FirebaseAuthMiddleware(RequestDelegate next, IFirebaseAuthService firebaseAuthService)
        {
            _next = next;
            _firebaseAuthService = firebaseAuthService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length);

                try
                {
                    var uid = await _firebaseAuthService.VerifyTokenAsync(token);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, uid)
                    };

                    var identity = new ClaimsIdentity(claims, "Firebase");
                    context.User = new ClaimsPrincipal(identity);
                }
                catch
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid Firebase token");
                    return;
                }
            }

            await _next(context);
        }
    }
}
