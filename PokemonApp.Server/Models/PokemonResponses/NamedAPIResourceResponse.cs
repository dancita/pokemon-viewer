using Newtonsoft.Json;

namespace PokemonApp.Server.Models.PokemonResponses
{
    [Serializable]
    public class NamedAPIResourceResponse
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }
}