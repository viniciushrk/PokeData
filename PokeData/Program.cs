using EventoApp.Core.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    "User ID=postgres;" +
    "Password=postgres;" +
    "Host=localhost;Port:5432;Database=pokemon_database;";
builder.Services.AddDbContext<PokemonContext>(options =>
    options.UseNpgsql(connectionString
    //,
    //o => o.MigrationsAssembly("EventoApp.Api")
    )); ;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
