using PokemonApp.Server.Models;
using PokemonApp.Server.Models.PokemonResponses;
using Type = PokemonApp.Server.Models.Type;

namespace PokemonApp.Server.Infrastructure.Mapping
{
    public static class PokemonMapper
    {
        public static Pokemon MapPokemon(PokemonResponse response)
        {
            var pokemon = new Pokemon
            {
                Name = response.Name,
                Height = response.Height,
                Weight = response.Weight,
                FrontDefaultImage = response.Sprites?.FrontDefaultImage
            };

            if (response.Types != null)
            {
                foreach (var typeDto in response.Types)
                {
                    pokemon.Types.Add(new PokemonType
                    {
                        Slot = typeDto.Slot,
                        Type = new Type
                        {
                            Name = typeDto?.Type?.Name,
                            Url = typeDto?.Type?.Url
                        }
                    });
                }
            }

            if (response.Abilities != null)
            {
                foreach (var abilityDto in response.Abilities)
                {
                    pokemon.Abilities.Add(new PokemonAbility
                    {
                        Slot = abilityDto.Slot,
                        IsHidden = abilityDto.IsHidden,
                        Ability = new Ability
                        {
                            Name = abilityDto?.Ability.Name,
                            Url = abilityDto?.Ability.Url
                        }
                    });
                }
            }

            return pokemon;
        }
    }
}