namespace PokemonApp.Server.Extensions
{
    public static class PokemonMapperExtensions
    {
        // For PokeApi url formats, like "https://pokeapi.co/api/v2/ability/65/"
        public static int ExtractIdFromUrl(string? url)
        {
            ArgumentNullException.ThrowIfNull(url);

            var parts = url.TrimEnd('/').Split('/');
            return int.TryParse(parts.Last(), out var id) ? id : throw new Exception($"Invalid URL: {url}");
        }
    }
}