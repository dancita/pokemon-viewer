using PokemonApp.Server.Models.PokemonResponses;

public static class PokemonTestData
{
    public static PokemonResponse GetSamplePokemon()
    {
        return new PokemonResponse
        {
            Name = "pikachu",
            Id = 1,
            Height = 55,
            Weight = 120,
            Sprites = new PokemonSpritesResponse
            {
                FrontDefaultImage = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/25.png"
            },
            Abilities = new List<PokemonAbilityResponse>
            {
                new PokemonAbilityResponse
                {
                    Ability = new NamedAPIResourceResponse
                    {
                        Name = "synchronize",
                        Url = "https://pokeapi.co/api/v2/ability/28/"
                    },
                    IsHidden = false,
                    Slot = 1
                },
                new PokemonAbilityResponse
                {
                    Ability = new NamedAPIResourceResponse
                    {
                        Name = "inner-focus",
                        Url = "https://pokeapi.co/api/v2/ability/39/"
                    },
                    IsHidden = true,
                    Slot = 2
                }
            },
            Types = new List<PokemonTypeResponse>
            {
                new PokemonTypeResponse
                {
                    Slot = 1,
                    Type = new NamedAPIResourceResponse
                    {
                        Name = "Fire",
                        Url = "https://pokeapi.co/api/v2/type/10/"
                    }
                }
            }
        };
    }
}