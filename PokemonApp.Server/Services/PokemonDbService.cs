using Microsoft.EntityFrameworkCore;
using PokemonApp.Server.Exceptions;
using PokemonApp.Server.Extensions;
using PokemonApp.Server.Infrastructure;
using PokemonApp.Server.Infrastructure.Mapping;
using PokemonApp.Server.Interfaces;
using PokemonApp.Server.Models;
using PokemonApp.Server.Models.PokemonResponses;
using System.Net;
using System.Security.Claims;

namespace PokemonApp.Server.Services
{
    public class PokemonDbService : IPokemonDbService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PokemonDbService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SavePokemonAsync(PokemonResponse response)
        {
            var pokemonExists = await GetStoredPokemonAsync(response.Id);

            if (!pokemonExists)
            {
                var pokemon = PokemonMapper.MapPokemon(response);
                pokemon = await AddAbilitiesAsync(pokemon, response.Abilities);

                _appDbContext.Pokemon.Add(pokemon);
            }

            _appDbContext.PokemonRequest.Add(new PokemonRequest
            {
                PokemonApiId = response.Id,
                CreatedBy = GetUserId()
            });

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new PokemonInfoException(HttpStatusCode.InternalServerError, ex.Message);
            }          
        }

        private async Task<bool> GetStoredPokemonAsync(int pokemonApiId)
        {
            return await _appDbContext.Pokemon.AnyAsync(p => p.PokemonApiId == pokemonApiId);
        }

        private string? GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        private async Task<Pokemon> AddAbilitiesAsync(Pokemon pokemon, List<PokemonAbilityResponse> abilityResponses)
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
    }
}