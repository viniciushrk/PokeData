using Microsoft.AspNetCore.Mvc;
using Domain.Data;
using Domain.Models;
using Domain.Dto;
using Infra.Services;
using Domain.Repository;

namespace PokeData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonContext _pokemonContext;
        private readonly SocketService _socketService;
        private readonly CacheService _cacheService;
        private readonly BuscarListaPokemonRepository _buscarListaPokemonRepository;
        public PokemonController(
            PokemonContext pokemonContext,
            SocketService socketService,
            CacheService cacheService,
            BuscarListaPokemonRepository buscarListaPokemonRepository
        )
        {
            _pokemonContext = pokemonContext;
            _socketService = socketService;
            _cacheService = cacheService;
            _buscarListaPokemonRepository = buscarListaPokemonRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var pokemons = await _cacheService.GetCacheListaPokemon();

            if(pokemons != null)
                return Ok(pokemons);    

            var pokemonDb = _buscarListaPokemonRepository.Executar();

            await _cacheService.EmitirCachePokemon();
            return Ok(pokemonDb);
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
        public async Task<IActionResult> PostAsync(
            [FromBody] CriarPokemonDto pokemon,
            [FromServices] CadastrarPokemonRepository cadastrarPokemonRepository
        ) {
            if (pokemon == null)
                return BadRequest();

            var pokemonCriado = cadastrarPokemonRepository.Executar(pokemon); 

            await _cacheService.EmitirCachePokemon();
            return Created($"eventos/{pokemonCriado.Id}", pokemon);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] Pokemon pokemon)
        {
            if (id != pokemon.Id)
                return BadRequest();
            
            _pokemonContext.Pokemon.Update(pokemon);
            _pokemonContext.SaveChanges();

            await _cacheService.EmitirCachePokemon();
            return NoContent();
        }

        [HttpPost("IncrementStars/{id}")]
        public IActionResult IncrementStars(
            Guid id
        ) {
            var pokemonSearch = _pokemonContext.Pokemon.Find(id);
            
            if (pokemonSearch == null)
                return BadRequest();

            pokemonSearch.IncrementStars();

            _pokemonContext.Pokemon.Update(pokemonSearch);
            _pokemonContext.SaveChanges();

            _socketService.EmitirEventoStarsIncrement(pokemon: pokemonSearch);
            
            return NoContent();
        }

        [HttpPost("DecrementStars/{id}")]
        public IActionResult DecrementStars(Guid id)
        {
            var pokemonSearch = _pokemonContext.Pokemon.Find(id);

            if (pokemonSearch == null)
                return BadRequest();
            
            if(pokemonSearch.Stars != 0)
            {
                pokemonSearch.DecrementStars();
                _pokemonContext.Pokemon.Update(pokemonSearch);
                _pokemonContext.SaveChanges();

                _socketService.EmitirEventoStarsDecrement(pokemon: pokemonSearch);
            }

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
