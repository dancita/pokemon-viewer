using PokemonApp.Server.Models.PokemonResponses;

namespace PokemonApp.Server.Models
{
    public class Type : BaseAPIEntity
    {
        public ICollection<PokemonType> PokemonTypes { get; set; } = new List<PokemonType>();
    }
}