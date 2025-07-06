using Newtonsoft.Json;

namespace PokemonApp.Server.Models
{
    public class PokemonSprites
    {
        [JsonProperty("front_default")]
        public string? FrontDefaultImage { get; set; }
    }
}
