
using Domain.Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class BuscarListaPokemonRepository
    {
        private readonly PokemonContext _pokemonContext;
        
        public BuscarListaPokemonRepository(
          PokemonContext pokemonContext
        ) {
            _pokemonContext = pokemonContext;
        }

        public List<Pokemon> Executar() 
        {
            var pokemonDb = _pokemonContext.Pokemon.ToList();
            return pokemonDb;
        }
    }
}
