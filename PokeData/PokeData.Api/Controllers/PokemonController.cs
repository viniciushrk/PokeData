using Microsoft.AspNetCore.Mvc;
using PokeData.Core;

namespace PokeData.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        //private readonly List<Pokemon> pokemons = new List<Pokemon>();

        //Pokemon pokemon = new Pokemon()
        //{
        //    Id = 1,
        //    Name = "Pikachu",
        //    Type = "Elétrico",
        //    Image = "http://pm1.narvii.com/6434/7a2cb5fc86df1db37db549422128c66186059808_00.jpg",
        //    Description = "É uma espécie fictícia pertencente à franquia de mídia Pokémon da Nintendo. Ele apareceu pela primeira vez no Japão em 1996, nos jogos eletrônicos Pokémon Red and Blue, e foi criado por Satoshi Tajiri.",
        //    Generation = "Primeira geração",
        //    Stars = 0,
        //};

        //[HttpGet(Name = "GetPokemon")]
        //public List<Pokemon> Get()
        //{
        //    Pokemon pokemon = new Pokemon()
        //    {
        //        Id = Random.Shared.Next(0, 55),
        //        Name = "Pikachu",
        //        Type = "Elétrico",
        //        Image = "http://pm1.narvii.com/6434/7a2cb5fc86df1db37db549422128c66186059808_00.jpg",
        //        Description = "É uma espécie fictícia pertencente à franquia de mídia Pokémon da Nintendo. Ele apareceu pela primeira vez no Japão em 1996, nos jogos eletrônicos Pokémon Red and Blue, e foi criado por Satoshi Tajiri.",
        //        Generation = "Primeira geração",
        //        Stars = 0,
        //    };

        //    pokemons.Add(pokemon);
        //    return pokemons;
        //}
    }
}
