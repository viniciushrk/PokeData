using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web;
using Domain.Services;
using Domain.Helper;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddRefitClient<IPokemonService>().ConfigureHttpClient(h => {
//    h.BaseAddress = new Uri("https://localhost:5003");
//});
builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri("https://localhost:5003/api/") 
});
// TO-DO remover essa merda
builder.Services.AddScoped<HttpHelperServico>();
builder.Services.AddScoped<ICriarPokemonService, CriarPokemonService>();

await builder.Build().RunAsync();
