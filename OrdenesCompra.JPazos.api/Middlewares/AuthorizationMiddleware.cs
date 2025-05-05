using OrdenesCompra.JPazos.application.generics;
using System.Net;
using System.Text.Json;

namespace OrdenesCompra.JPazos.api.middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            await _next(httpContext);
            var statusCode = httpContext.Response.StatusCode;
            if (statusCode == (int)HttpStatusCode.Unauthorized || statusCode == (int) HttpStatusCode.Forbidden)
            {
                httpContext.Response.ContentType = "application/json";
                var exceptionResponse = new ExceptionResponse("Necesitas loguearte para acceder a este recurso.", "00000401");

                if (statusCode == (int)HttpStatusCode.Forbidden) exceptionResponse = new ExceptionResponse("Necesitas un privilegio elevado para acceder a este recurso.", "00000403");

                var exceptionResponseJson = JsonSerializer.Serialize(exceptionResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                await httpContext.Response.WriteAsync(exceptionResponseJson);
            }
        }
    }
}
