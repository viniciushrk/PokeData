
using Domain.Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class EditarPokemonRepository
    {
        private readonly PokemonContext _pokemonContext;
        
        public EditarPokemonRepository(
          PokemonContext pokemonContext
        ) {
            _pokemonContext = pokemonContext;
        }

        public void Executar(Pokemon pokemon) 
        {
            _pokemonContext.Pokemon.Update(pokemon);
            _pokemonContext.SaveChanges();
        }
    }
}
