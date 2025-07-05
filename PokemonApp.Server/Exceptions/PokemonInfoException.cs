using System.Net;

namespace PokemonApp.Server.Exceptions
{
    public class PokemonInfoException : HttpRequestException
    {
        public HttpStatusCode StatusCode { get; }

        public PokemonInfoException(HttpStatusCode statusCode, string? message = null)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}