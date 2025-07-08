using Microsoft.EntityFrameworkCore;
using PokemonApp.Server.Extensions;
using PokemonApp.Server.Infrastructure;
using PokemonApp.Server.Infrastructure.Mapping;
using PokemonApp.Server.Interfaces;
using PokemonApp.Server.Models;
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
            var pokemonExists = await PokemonExistsInDb(response.Id);

            if (!pokemonExists)
            {
                var pokemon = PokemonMapper.MapPokemon(response);
                pokemon = await AddAbilities(pokemon, response.Abilities);

                _appDbContext.Pokemon.Add(pokemon);
            }

            _appDbContext.PokemonRequest.Add(new PokemonRequest
            {
                PokemonId = response.Id,
                CreatedBy = "" //TODO get the user id
            });

            await _appDbContext.SaveChangesAsync(); //TODO add try catch
        }

        private async Task<Pokemon?> GetStoredPokemon(int pokemonId)
        {
            return await _appDbContext.Pokemon
                .Include(p => p.Types)
                .Include(p => p.Abilities)
                .FirstOrDefaultAsync(p => p.Id == pokemonId);
        }

        private async Task<Pokemon> AddAbilities(Pokemon pokemon, List<PokemonAbilityResponse> abilityResponses)
        {
            if (abilityResponses != null)
            {
                
                foreach (var abilityResponse in abilityResponses)
                {
                    var abilityId = PokemonMapperExtensions.ExtractIdFromUrl(abilityResponse?.Ability?.Url);
                    var existingAbility = await _appDbContext.Ability.FirstOrDefaultAsync(a => a.AbilityApiId == abilityId);

                    if (existingAbility == null)
                    {
                        existingAbility = new Ability
                        {
                            AbilityApiId = abilityId,
                            Name = abilityResponse.Ability.Name,
                            Url = abilityResponse.Ability.Url
                        };

                        _appDbContext.Ability.Add(existingAbility);
                    }

                    pokemon.Abilities.Add(new PokemonAbility
                    {
                        Slot = abilityResponse.Slot,
                        IsHidden = abilityResponse.IsHidden,
                        Ability = existingAbility
                    });
                }
            }

            return pokemon;
        }

        private async Task<bool> PokemonExistsInDb(int pokemonId)
        {
            return await GetStoredPokemon(pokemonId) != null;
        }
    }
}