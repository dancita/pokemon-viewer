using PokemonApp.Server.Models.PokemonResponses;

namespace PokemonApp.Server.Interfaces
{
    public interface IPokemonDbService
    {
        public Task SavePokemonAsync(PokemonResponse pokemonResponse);
    }
}