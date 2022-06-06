
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
    public class CadastrarPokemonRepository
    {
        private readonly PokemonContext _pokemonContext;
        
        public CadastrarPokemonRepository(
          PokemonContext pokemonContext
        ) {
            _pokemonContext = pokemonContext;
        }

        public Pokemon Executar(CriarPokemonDto pokemonDto) 
        {
            Pokemon pokemonModel = new Pokemon()
            {
                Description = pokemonDto.Description,
                Image = pokemonDto.Image,
                Type = pokemonDto.Type,
                Generation = pokemonDto.Generation,
                Nome = pokemonDto.Nome,
            };

            _pokemonContext.Pokemon.Add(pokemonModel);
            _pokemonContext.SaveChanges();

            return pokemonModel;
        }
    }
}
