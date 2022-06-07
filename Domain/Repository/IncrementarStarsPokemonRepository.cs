
using Domain.Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class IncrementarStarsPokemonRepository
    {
        private readonly PokemonContext _pokemonContext;
        
        public IncrementarStarsPokemonRepository(
          PokemonContext pokemonContext
        ) {
            _pokemonContext = pokemonContext;
        }

        public Pokemon Executar(Pokemon pokemon) 
        {
            pokemon.IncrementStars();

            _pokemonContext.Pokemon.Update(pokemon);
            _pokemonContext.SaveChanges();

            return pokemon;
        }
    }
}
