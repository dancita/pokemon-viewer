using Newtonsoft.Json;
using System.Text.Json;

namespace PokemonApp.Server.Models.PokemonResponses
{
    [Serializable]
    public class PokemonResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("sprites")]
        public PokemonSpritesResponse? Sprites { get; set; }

        [JsonProperty("abilities")]
        public List<PokemonAbilityResponse>? Abilities { get; set; }

        [JsonProperty("types")]
        public List<PokemonTypeResponse>? Types { get; set; }
    }
}