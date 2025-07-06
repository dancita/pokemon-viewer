using Newtonsoft.Json;
using System.Text.Json;

namespace PokemonApp.Server.Models
{
    [Serializable]
    public class Pokemon
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("abilities")]
        public List<PokemonAbility>? Abilities { get; set; }

        [JsonProperty("types")]
        public List<PokemonType>? Types { get; set; }

        [JsonProperty("sprites")]
        public PokemonSprites? Sprites { get; set; }
    }
}