using Microsoft.EntityFrameworkCore;
using PokeData.Core.Models;

namespace PokeData.Core.Data;
public class PokemonContext : DbContext
{
    public PokemonContext(DbContextOptions<PokemonContext> options): base(options){}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.UseSerialColumns();
        base.OnModelCreating(builder);
    }
    public DbSet<Pokemon> Pokemon { get; set; }

}
