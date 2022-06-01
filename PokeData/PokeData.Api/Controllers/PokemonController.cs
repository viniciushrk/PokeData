using Microsoft.AspNetCore.Mvc;
using PokeData.Core;

namespace PokeData.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly List<Pokemon> pokemons;

        Pokemon pokemon = new Pokemon()
        {
            Id = 1,
            Name = "Pikachu",
            Type = "Elétrico",
            Image = "http://pm1.narvii.com/6434/7a2cb5fc86df1db37db549422128c66186059808_00.jpg",

        };
    }
}
