namespace PokemonApp.Server.Models
{
    public class Ability : BaseAPIEntity
    {
        public int AbilityApiId { get; set; }

        public ICollection<PokemonAbility> PokemonAbilities { get; set; } = new List<PokemonAbility>();
    }
}