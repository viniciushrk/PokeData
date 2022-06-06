using Domain.Dto;
using Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Services
{
    public class SocketService
    {
        public async Task EmitirEventoStarsIncrement(Pokemon pokemon)
        {
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379"))
            {
                ISubscriber sub = redis.GetSubscriber();
                var message = new MessageSocket()
                {
                    Event = "pokemon",
                    Room = "pokemon_post",
                    Data = new { 
                        message = $"Pokemon {pokemon.Nome} recebeu uma estrela",
                        id = pokemon.Id,
                        stars = pokemon.Stars
                    }
                };

                var serializerSettings = new JsonSerializerSettings();

                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                sub.Publish("pokemon", JsonConvert.SerializeObject(message, serializerSettings));
            }
        }

        public async Task EmitirEventoStarsDecrement(Pokemon pokemon)
        {
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379"))
            {
                ISubscriber sub = redis.GetSubscriber();
                var message = new MessageSocket()
                {
                    Event = "pokemon",
                    Room = "pokemon_post",
                    Data = new
                    {
                        message = $"Pokemon {pokemon.Nome} perdeu uma estrela :(",
                        id = pokemon.Id,
                        stars = pokemon.Stars
                    }
                };

                var serializerSettings = new JsonSerializerSettings();

                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                sub.Publish("pokemon", JsonConvert.SerializeObject(message, serializerSettings));
            }
        }
    }
}
