using System.Net;

namespace PokemonApp.Server.Exceptions
{
    public class PokemonInfoException : HttpRequestException
    {
        public PokemonInfoException(HttpStatusCode statusCode, string? message = null)
            : base(message, null, statusCode)
        {
        }
    }
}