using Microsoft.AspNetCore.Mvc;
using PokeData.Core.Data;
using PokeData.Core.Models;
using System.Linq;

namespace PokeData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonContext _pokemonContext;
        public PokemonController(PokemonContext pokemonContext)
        {
            _pokemonContext = pokemonContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_pokemonContext.Pokemon);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var pokemon = _pokemonContext.Pokemon.Find(id);
            if (pokemon == null)
                return NotFound();
            return Ok(pokemon);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Pokemon pokemon)
        {
            if (pokemon == null)
                return BadRequest();
            _pokemonContext.Pokemon.Add(pokemon);
            _pokemonContext.SaveChanges();

            return Created($"eventos/{pokemon.Id}", pokemon);
        }


        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Pokemon pokemon)
        {   
            if (id != pokemon.Id)
                return BadRequest();
            
            _pokemonContext.Pokemon.Update(pokemon);
            _pokemonContext.SaveChanges();

            return NoContent();
        }

        [HttpPost("IncrementStars/{id}")]
        public IActionResult IncrementStars(Guid id)
        {
            var pokemonSearch = _pokemonContext.Pokemon.Find(id);
            
            if (pokemonSearch == null)
                return BadRequest();

            pokemonSearch.IncrementStars();

            _pokemonContext.Pokemon.Update(pokemonSearch);
            _pokemonContext.SaveChanges();

            return NoContent();
        }

        [HttpPost("DecrementStars/{id}")]
        public IActionResult DecrementStars(Guid id)
        {
            var pokemonSearch = _pokemonContext.Pokemon.Find(id);

            if (pokemonSearch == null)
                return BadRequest();

            pokemonSearch.DecrementStars();

            _pokemonContext.Pokemon.Update(pokemonSearch);
            _pokemonContext.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var evento = _pokemonContext.Pokemon.Find(id);
            if (evento == null)
                return NotFound();
            _pokemonContext.Pokemon.Remove(evento);
            _pokemonContext.SaveChanges();
            return NoContent();
        }
    }
}
