using Microsoft.AspNetCore.Mvc;
using PokemonApp.Server.Interfaces;
using PokemonApp.Server.Models;

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
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Query parameter is required.");
            }

            Pokemon pokemon = await _pokemonInfoService.GetPokemonAsync(query);

            if (pokemon == null)
            {
                return NotFound("No pokemons found.");
            }

            return Ok(pokemon);
        }
    }
}