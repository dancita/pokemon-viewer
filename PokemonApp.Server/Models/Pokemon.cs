namespace PokemonApp.Server.Models
{
    public class Pokemon
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        public string? FrontDefaultImage { get; set; }

        public ICollection<PokemonType> Types { get; set; } = new List<PokemonType>();

        public ICollection<PokemonAbility> Abilities { get; set; } = new List<PokemonAbility>();
    }
}