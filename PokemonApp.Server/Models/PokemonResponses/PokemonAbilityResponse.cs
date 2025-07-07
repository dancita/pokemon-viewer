using Newtonsoft.Json;

namespace PokemonApp.Server.Models.PokemonResponses
{
    [Serializable]
    public class PokemonAbilityResponse
    {
        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("slot")]
        public int Slot { get; set; }

        [JsonProperty("ability")]
        public NamedAPIResourceResponse? Ability { get; set; }
    }
}