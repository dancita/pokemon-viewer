using PokemonApp.Server.Infrastructure;
using PokemonApp.Server.Infrastructure.Mapping;
using PokemonApp.Server.Interfaces;
using PokemonApp.Server.Models.PokemonResponses;


namespace PokemonApp.Server.Services
{
    public class PokemonDbService : IPokemonDbService
    {
        private readonly AppDbContext _appDbContext;

        public PokemonDbService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task SavePokemonAsync(PokemonResponse response)
        {
            var pokemon = PokemonMapper.MapPokemon(response);

            _appDbContext.Pokemon.Add(pokemon);
            await _appDbContext.SaveChangesAsync();
        }
    }
}