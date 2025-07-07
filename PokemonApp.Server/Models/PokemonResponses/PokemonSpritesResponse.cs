using Newtonsoft.Json;

namespace PokemonApp.Server.Models.PokemonResponses
{
    public class PokemonSpritesResponse
    {
        [JsonProperty("front_default")]
        public string? FrontDefaultImage { get; set; }
    }
}
