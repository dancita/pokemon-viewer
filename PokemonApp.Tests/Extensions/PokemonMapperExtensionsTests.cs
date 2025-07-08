using PokemonApp.Server.Extensions;

namespace PokemonApp.Tests.Extensions
{
    public class PokemonMapperExtensionsTests
    {
        [Fact]
        public void ExtractIdFromUrl_ValidUrl_ReturnsId()
        {
            string url = "https://pokeapi.co/api/v2/ability/65/";

            int id = PokemonMapperExtensions.ExtractIdFromUrl(url);

            Assert.Equal(65, id);
        }

        [Fact]
        public void ExtractIdFromUrl_InvalidUrl_ThrowsException()
        {
            string url = "https://pokeapi.co/api/v2/ability/notanumber/";

            var ex = Assert.Throws<Exception>(() => PokemonMapperExtensions.ExtractIdFromUrl(url));
            Assert.Equal("Invalid URL: https://pokeapi.co/api/v2/ability/notanumber/", ex.Message);
        }

        [Fact]
        public void ExtractIdFromUrl_NullUrl_ThrowsArgumentNullException()
        {
            string? url = null;

            Assert.Throws<ArgumentNullException>(() => PokemonMapperExtensions.ExtractIdFromUrl(url));
        }
    }
}