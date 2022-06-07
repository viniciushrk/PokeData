using Domain.Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class BuscaPokemonRepository
    {
        private readonly PokemonContext _pokemonContext;
        
        public BuscaPokemonRepository(
          PokemonContext pokemonContext
        ) {
            _pokemonContext = pokemonContext;
        }

        public Pokemon Executar(Guid id) 
        {
            var pokemonDb = _pokemonContext.Pokemon.Find(id);
            return pokemonDb;
        }
    }
}
