namespace PokemonApp.Server.Models
{
    public class Ability : BaseAPIEntity
    {
        public ICollection<PokemonAbility> PokemonAbilities { get; set; } = new List<PokemonAbility>();
    }
}