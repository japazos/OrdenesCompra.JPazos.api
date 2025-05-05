using OrdenesCompra.JPazos.application.generics;
using OrdenesCompra.JPazos.application.IServices;
using System.Security.Claims;
using System.Text.Json;

namespace OrdenesCompra.JPazos.api.middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISecurityService _securityService;

        public JwtMiddleware(RequestDelegate next, ISecurityService securityService)
        {
            _next = next;
            _securityService = securityService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(httpContext.User?.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {
               var tokenFromRequest = httpContext.Request.Headers.Authorization.ToString().Replace("Bearer", "").Replace(" ", "");
               var email = identity.FindFirst(ClaimTypes.Email);
               var currentUser = await _securityService.GetUserByEmail(email!.ToString());
               if (currentUser != null) {
                    if (!tokenFromRequest.Equals(currentUser.Token)) {
                        httpContext.Response.ContentType = "application/json";
                        var exceptionResponse = new ExceptionResponse("Necesitas loguearte para acceder a este recurso.", "00000401");
                        var exceptionResponseJson = JsonSerializer.Serialize(exceptionResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        await httpContext.Response.WriteAsync(exceptionResponseJson);
                    }
                }
            }
            await _next(httpContext);
        }
    }
}
