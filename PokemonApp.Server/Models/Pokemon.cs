namespace PokemonApp.Server.Models
{
    public class Pokemon
    {
        public string Name { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        public List<PokemonAbility> Abilities { get; set; }

        public List<PokemonType> Types { get; set; }
    }
}
