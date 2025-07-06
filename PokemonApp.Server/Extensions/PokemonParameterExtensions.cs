using System.Text.RegularExpressions;

namespace PokemonApp.Server.Extensions
{
    public static class PokemonParameterExtensions
    {
        private static readonly Regex ValidPattern = new(@"^[a-zA-Z0-9-]+$");

        public static bool IsValidFormat(this string identifier) =>
             !string.IsNullOrWhiteSpace(identifier) && ValidPattern.IsMatch(identifier);
        
        public static bool IsValidLength(this string identifier, int maxLength = 12) =>
            identifier.Length <= maxLength;
    }
}