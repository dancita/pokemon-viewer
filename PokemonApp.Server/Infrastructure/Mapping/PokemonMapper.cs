using PokemonApp.Server.Extensions;
using PokemonApp.Server.Models;
using PokemonApp.Server.Models.PokemonResponses;

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
                FrontDefaultImage = response.Sprites?.FrontDefaultImage,
                PokemonApiId = response.Id
            };

            if (response.Types != null)
            {
                foreach (var typeDto in response.Types)
                {
                    var typeId = PokemonMapperExtensions.ExtractIdFromUrl(typeDto?.Type?.Url);

                    pokemon.Types.Add(new PokemonType
                    {
                        Pokemon = pokemon,
                        TypeId = typeId,
                        Slot = typeDto.Slot
                    });
                }
            }

            return pokemon;
        }
    }
}