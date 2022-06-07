
using Domain.Data;
using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class DeletarPokemonRepository
    {
        private readonly PokemonContext _pokemonContext;
        
        public DeletarPokemonRepository(
          PokemonContext pokemonContext
        ) {
            _pokemonContext = pokemonContext;
        }

        public void Executar(Pokemon pokemon) 
        {
            _pokemonContext.Pokemon.Remove(pokemon);
            _pokemonContext.SaveChanges();
        }
    }
}
