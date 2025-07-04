using Newtonsoft.Json;

namespace PokemonApp.Server.Models
{
    [Serializable]
    public class NamedAPIResource
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
