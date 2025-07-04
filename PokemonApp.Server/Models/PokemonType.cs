using Newtonsoft.Json;

namespace PokemonApp.Server.Models
{
    [Serializable]
    public class PokemonType
    {
        [JsonProperty("slot")]
        public int Slot { get; set; }

        [JsonProperty("type")]
        public NamedAPIResource Type { get; set; }
    }
}
