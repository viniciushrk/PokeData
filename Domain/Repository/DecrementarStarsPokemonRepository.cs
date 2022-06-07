
using Domain.Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class DecrementarStarsPokemonRepository
    {
        private readonly PokemonContext _pokemonContext;
        
        public DecrementarStarsPokemonRepository(
          PokemonContext pokemonContext
        ) {
            _pokemonContext = pokemonContext;
        }

        public Pokemon Executar(Pokemon pokemon) 
        {
            if (pokemon.Stars != 0)
            {
                pokemon.DecrementStars();
                _pokemonContext.Pokemon.Update(pokemon);
                _pokemonContext.SaveChanges();
            }

            return pokemon;
        }
    }
}
