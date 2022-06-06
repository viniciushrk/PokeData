using Domain.Dto;
using Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Data;

namespace Infra.Services
{
    public class CacheService
    {
        private readonly IDistributedCache _cache;
        private readonly PokemonContext _context;
        
        public CacheService(
            IDistributedCache cache,
            PokemonContext context
        ) {
            _cache = cache;
            _context = context;
        }
        public async Task EmitirCachePokemon()
        {
            string cacheKey = $"lista_pokemon";

            _cache.Remove(cacheKey);

            var pokemons = _context.Pokemon.ToList();

            string cachedDataString = JsonSerializer.Serialize(pokemons);
            var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(50))
                .SetSlidingExpiration(TimeSpan.FromMinutes(3));

            await _cache.SetAsync(cacheKey, dataToCache, options);
        }

        public async Task<List<Pokemon>> GetCacheListaPokemon()
        {
            string cacheKey = $"lista_pokemon";

            byte[] cachedData = await _cache.GetAsync(cacheKey);

            _cache.Remove(cacheKey);
            
            if (cachedData != null)
            {
                var cachePokemons = Encoding.UTF8.GetString(cachedData);
                return JsonSerializer.Deserialize<List<Pokemon>>(cachePokemons);
            }

            return null;
        }
    }
}
