using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace UserRegistrationApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrió un error: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            // Definimos el código de error HTTP
            response.StatusCode = exception switch
            {
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                Npgsql.PostgresException => (int)HttpStatusCode.Conflict, 
                _ => (int)HttpStatusCode.InternalServerError,
            };

            var errorResponse = new
            {
                error = exception.Message,
                details = exception is Npgsql.PostgresException npgsqlEx
                    ? new
                    {
                        sqlState = npgsqlEx.SqlState,
                        message = npgsqlEx.Message,
                    }
                    : null
            };

            return response.WriteAsJsonAsync(errorResponse);
        }
    }
}
