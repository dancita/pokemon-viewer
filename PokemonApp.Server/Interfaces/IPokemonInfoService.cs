using PokemonApp.Server.Models.PokemonResponses;

namespace PokemonApp.Server.Interfaces
{
    public interface IPokemonInfoService
    {
        Task<PokemonResponse> GetPokemonAsync(string query);
    }
}