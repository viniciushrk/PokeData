using Domain.Data;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Infra.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PokemonContext>(options =>
    //options.UseSqlite("Data Source=Pokemon.db",o => o.MigrationsAssembly("PokeData.Core")
    options.UseNpgsql(builder.Configuration.GetConnectionString("PokemonDB"),o => o.MigrationsAssembly("Domain")
    ));

builder.Services.AddStackExchangeRedisCache(
    options => { 
        options.Configuration = "localhost:6379"; 
    });
// Add services to the container.

builder.Services.AddScoped<BuscaPokemonRepository>();
builder.Services.AddScoped<BuscarListaPokemonRepository>();
builder.Services.AddScoped<CadastrarPokemonRepository>();
builder.Services.AddScoped<DecrementarStarsPokemonRepository>();
builder.Services.AddScoped<DeletarPokemonRepository>();
builder.Services.AddScoped<DecrementarStarsPokemonRepository>();
builder.Services.AddScoped<EditarPokemonRepository>();
builder.Services.AddScoped<IncrementarStarsPokemonRepository>();

builder.Services.AddScoped<SocketService>();
builder.Services.AddScoped<CacheService>();
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

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
