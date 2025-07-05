using PokemonApp.Server.Exceptions;
using System.Net;

namespace PokemonApp.Server.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                _logger.LogError(ex, "[ExceptionMiddleware] An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode;
            string message;

            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case PokemonInfoException ex:
                    statusCode = (int)ex.StatusCode;
                    message = ex.StatusCode switch
                    {
                        HttpStatusCode.BadRequest => "Bad request sent to the external API.",
                        HttpStatusCode.Unauthorized => "You are not authorized to access this resource.",
                        HttpStatusCode.Forbidden => "Access to the external API was forbidden.",
                        HttpStatusCode.NotFound => "The requested Pokémon was not found.",
                        HttpStatusCode.InternalServerError => "External API encountered an internal error.",
                        HttpStatusCode.ServiceUnavailable => "The service is temporarily unavailable.",
                        _ => "An error occurred when calling the external API."
                    };
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    message = "An internal server error occurred.";
                    break;
            }

            context.Response.StatusCode = statusCode;

            var response = new 
            {
                statusCode,
                message
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}