using Microsoft.AspNetCore.Mvc;
using PokemonApp.Server.Interfaces;

namespace PokemonApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonInfoService _pokemonInfoService;

        public PokemonController(IPokemonInfoService pokemonInfoService)
        {
            _pokemonInfoService = pokemonInfoService;
        }

        [HttpGet("{query}")]
        public async Task<IActionResult> GetPokemon(string query)
        {
            var pokemon = await _pokemonInfoService.GetPokemonAsync(query);

            return Ok(pokemon);
        }
    }
}