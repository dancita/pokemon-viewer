namespace PokemonApp.Server.Models
{
    public class PokemonAbility
    {
        public int PokemonId { get; set; }

        public int AbilityId { get; set; }

        public int Slot { get; set; }

        public bool IsHidden { get; set; }

        public Pokemon Pokemon { get; set; } = null!;

        public Ability Ability { get; set; } = null!;
    }
}