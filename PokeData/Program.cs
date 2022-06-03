using PokeData.Core.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//var connectionString =
//    "User ID=postgres;Password=postgres;Host=localhost;Port:5432;Database=pokemon_database;";

    
builder.Services.AddDbContext<PokemonContext>(options =>
    options.UseSqlite("Data Source=Pokemon.db",
    o => o.MigrationsAssembly("PokeData.Core")
    //options.UseNpgsql(builder.Configuration.GetConnectionString("PokemonDB")/*,o => o.MigrationsAssembly("PokeData.Core")*/
    ));
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(
    x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
