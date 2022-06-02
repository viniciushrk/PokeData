using Microsoft.EntityFrameworkCore;
using PokeData.Core;

namespace EventoApp.Core.Data;
public class PokemonContext : DbContext
{
    public DbSet<Pokemon> Eventos { get; set; }
    public PokemonContext(DbContextOptions<PokemonContext> options)
        : base(options)
    {

    }
}
