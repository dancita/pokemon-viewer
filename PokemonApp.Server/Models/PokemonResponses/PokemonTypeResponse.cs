using Newtonsoft.Json;

namespace PokemonApp.Server.Models.PokemonResponses
{
    [Serializable]
    public class PokemonTypeResponse
    {
        [JsonProperty("slot")]
        public int Slot { get; set; }

        [JsonProperty("type")]
        public NamedAPIResourceResponse? Type { get; set; }
    }
}