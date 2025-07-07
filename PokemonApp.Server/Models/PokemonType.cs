namespace PokemonApp.Server.Models
{
    public class PokemonType
    {
        public int PokemonId { get; set; }

        public int TypeId { get; set; }

        public int Slot { get; set; }

        public Pokemon Pokemon { get; set; } = null!;

        public Type Type { get; set; } = null!;
    }
}