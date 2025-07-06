using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Server.Interfaces;

namespace PokemonApp.Server.Controllers
{
    /// <summary>
    /// API controller that handles Pokemon operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonInfoService _pokemonInfoService;

        public PokemonController(IPokemonInfoService pokemonInfoService)
        {
            _pokemonInfoService = pokemonInfoService;
        }

        /// <summary>
        /// Retrieves details of a Pokemon
        /// </summary>
        /// <param name="identifier">Id or name</param>
        /// <returns>Returns Pokemon</returns>
        [HttpGet("{identifier}")]
        [Authorize(Policy = "read:pokemon")]
        public async Task<IActionResult> GetPokemon(string identifier)
        {
            var pokemon = await _pokemonInfoService.GetPokemonAsync(identifier);

            return Ok(pokemon);
        }
    }
}