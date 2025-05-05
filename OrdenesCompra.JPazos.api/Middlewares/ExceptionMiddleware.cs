using Azure;
using Microsoft.Data.SqlClient;
using OrdenesCompra.JPazos.application.Exceptions;
using OrdenesCompra.JPazos.application.generics;
using System.Net;

namespace OrdenesCompra.JPazos.api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandlerException(httpContext, exception);

            }

        }

        private async Task HandlerException(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            ExceptionResponse response
                = new ExceptionResponse("Ocurrió un error inesperado, por favor intente más tarde.");

            switch (exception)
            {
                case BusinessException businessException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new ExceptionResponse(businessException.Message, "BUSINESS_ERROR");
                    break;
                case SqlException sqlException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = new ExceptionResponse(sqlException.Message, "SQL_ERROR");
                    break;
                default:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = new ExceptionResponse("Ocurrió un error inesperado", "INTERNAL_ERROR");
                    break;
            }
            var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);
            await httpContext.Response.WriteAsync(jsonResponse);  
            await httpContext.Response.Body.FlushAsync();
        }
    }
}
