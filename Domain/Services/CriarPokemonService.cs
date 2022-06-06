using Domain.Dto;
using Domain.Helper;
using Domain.Models;
using System.Text.Json;


namespace Domain.Services
{
    public class CriarPokemonService : ICriarPokemonService
    {
        private readonly string _url;
        private readonly HttpHelperServico _httpHelperServico;

        public CriarPokemonService(HttpHelperServico httpHelperServico)
        {
            _url = "https://localhost:5003/api/";
            _httpHelperServico = httpHelperServico;
        }

        public async Task<Pokemon> ExecutarAsync(CriarPokemonDto pokemon)
        {
            var response = await _httpHelperServico.ExecuteRequestAsync(
                uri: _url,
                rota: "/Pokemon",
                body: pokemon,
                method: RestSharp.Method.POST,
                headers: null);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };

            var objeto = System.Text.Json.JsonSerializer.Deserialize<Pokemon>(response.Content, options);

            return objeto;
        }
    }
}
