using PokemonApp.Server.Models;

namespace PokemonApp.Server.Interfaces
{
    public interface IPokemonInfoService
    {
        Task<Pokemon> GetPokemonAsync(string query);
    }
}
