using Domain.Dto;
using Domain.Models;

namespace Domain.Services
{
    public interface ICriarPokemonService
    {
        Task<Pokemon> ExecutarAsync(CriarPokemonDto pokemon);
    }
}
