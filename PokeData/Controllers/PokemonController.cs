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
        private readonly BuscaPokemonRepository _buscaPokemon;
        public PokemonController(
            PokemonContext pokemonContext,
            SocketService socketService,
            CacheService cacheService,
            BuscarListaPokemonRepository buscarListaPokemonRepository,
            BuscaPokemonRepository buscaPokemon
        )
        {
            _pokemonContext = pokemonContext;
            _socketService = socketService;
            _cacheService = cacheService;
            _buscarListaPokemonRepository = buscarListaPokemonRepository;
            _buscaPokemon = buscaPokemon;
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
            var pokemon = _buscaPokemon.Executar(id);
            
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
        public async Task<IActionResult> PutAsync(
            Guid id,
            [FromBody] Pokemon pokemon,
            [FromServices] EditarPokemonRepository editarPokemon
        ) {
            if (id != pokemon.Id)
                return BadRequest();

            editarPokemon.Executar(pokemon);
            await _cacheService.EmitirCachePokemon();
            return NoContent();
        }

        [HttpPost("IncrementStars/{id}")]
        public async Task<IActionResult> IncrementStarsAsync(
            Guid id,
            [FromServices] IncrementarStarsPokemonRepository incrementarStarsPokemon
        ) {
            var pokemonSearch = _pokemonContext.Pokemon.Find(id);
            
            if (pokemonSearch == null)
                return BadRequest();

            var pokemonIncrementado = incrementarStarsPokemon.Executar(pokemonSearch);

            _socketService.EmitirEventoStarsIncrement(pokemon: pokemonIncrementado);

            await _cacheService.EmitirCachePokemon();
            return NoContent();
        }

        [HttpPost("DecrementStars/{id}")]
        public async Task<IActionResult> DecrementStarsAsync(
            Guid id,
            [FromServices] DecrementarStarsPokemonRepository decrementarStarsPokemon
        ) {
            var pokemonSearch = _pokemonContext.Pokemon.Find(id);

            if (pokemonSearch == null)
                return BadRequest();

            var pokemonDecrementado = decrementarStarsPokemon.Executar(pokemonSearch);

            _socketService.EmitirEventoStarsDecrement(pokemon: pokemonDecrementado);
            await _cacheService.EmitirCachePokemon();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(
            Guid id,
            [FromServices] DeletarPokemonRepository deletarPokemon
        ){

            var pokemon = _buscaPokemon.Executar(id);

            if (pokemon == null)
                return NotFound();

            deletarPokemon.Executar(pokemon);
            await _cacheService.EmitirCachePokemon();
            return NoContent();
        }
    }
}
