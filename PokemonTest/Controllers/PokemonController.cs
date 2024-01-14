

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using PokemonTest.Interfaces;
using PokemonTest.Models;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace PokemonTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonController(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        [HttpGet("api/pokemons")]
        [ProducesResponseType(200, Type = typeof(ICollection<Pokemon>))]
        public IActionResult GetAllPokemons()
        {
            var pokemons = _pokemonRepository.GetAllPokemons();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }
    }
}
