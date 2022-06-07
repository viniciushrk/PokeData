using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Domain.Data;
public class PokemonContext : DbContext
{
    public PokemonContext(DbContextOptions<PokemonContext> options): base(options){}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    public DbSet<Pokemon> Pokemon { get; set; }

}
