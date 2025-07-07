using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Server.Extensions;
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
        private readonly IPokemonDbService _pokemonDbService;

        public PokemonController(IPokemonInfoService pokemonInfoService, IPokemonDbService pokemonDbService)
        {
            _pokemonInfoService = pokemonInfoService;
            _pokemonDbService = pokemonDbService;
        }

        /// <summary>
        /// Retrieves details of a Pokemon and writes them to the db.
        /// </summary>
        /// <param name="identifier">Id or name</param>
        /// <returns>Returns Pokemon</returns>
        [HttpGet("{identifier}")]
        [Authorize(Policy = "read:pokemon")]
        public async Task<IActionResult> GetPokemon(string identifier)
        {
            if (!identifier.IsValidFormat())
            {
                return BadRequest("Only letters, numbers and hyphens are allowed.");
            }
            if (!identifier.IsValidLength())
            {
                return BadRequest("Maximum 12 characters are allowed.");
            }

            var pokemon = await _pokemonInfoService.GetPokemonAsync(identifier);
            await _pokemonDbService.SavePokemonAsync(pokemon);

            return Ok(pokemon);
        }
    }
}