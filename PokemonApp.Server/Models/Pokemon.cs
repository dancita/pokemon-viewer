namespace PokemonApp.Server.Models
{
    public class Pokemon
    {
        //PokeApi ID
        public int Id { get; set; }

        public string? Name { get; set; }

        //Height is in decimeters - 1 decimeter = 0.1 meter
        public int Height { get; set; }

        //Weight in hectograms - 1 hectogram = 0.1 kilogram
        public int Weight { get; set; }

        public string? FrontDefaultImage { get; set; }

        public ICollection<PokemonType> Types { get; set; } = new List<PokemonType>();

        public ICollection<PokemonAbility> Abilities { get; set; } = new List<PokemonAbility>();
    }
}