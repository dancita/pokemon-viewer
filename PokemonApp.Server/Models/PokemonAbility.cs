using Newtonsoft.Json;

namespace PokemonApp.Server.Models
{
    [Serializable]
    public class PokemonAbility
    {
        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("slot")]
        public int Slot { get; set; }

        [JsonProperty("ability")]
        public NamedAPIResource? Ability { get; set; }
    }
}
