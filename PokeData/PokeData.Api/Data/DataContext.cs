using Microsoft.EntityFrameworkCore;
using PokeData.Core;

namespace PokeData.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>().HasData(
            new Pokemon()
            {
                Id = 1,
                Name = "Pikachu",
                Type = "Elétrico",
                Image = "http://pm1.narvii.com/6434/7a2cb5fc86df1db37db549422128c66186059808_00.jpg",
                Description = "É uma espécie fictícia pertencente à franquia de mídia Pokémon da Nintendo. Ele apareceu pela primeira vez no Japão em 1996, nos jogos eletrônicos Pokémon Red and Blue, e foi criado por Satoshi Tajiri.",
                Generation = "Primeira geração",
                Stars = 0,
            },
            new Pokemon()
            {
                Id = 2,
                Name = "Kakuna",
                Type = "Inseto",
                Image = "https://assets.pokemon.com/assets/cms2/img/pokedex/full/014.png",
                Description = "é um Pokémon dos tipos Inseto e Venenoso, categorizado como Pokémon Casulo e introduzido na Primeira Geração.",
                Generation = "Primeira geração",
                Stars = 0,
            }
            );
        }
        public DbSet<Pokemon> Pokemons { get; set; }
    }
}
