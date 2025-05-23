using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Service.Contract;

namespace Talabat.Service.Services.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<string> GetCacheAsync(string Key)
        {
            var cacheResponse = await _database.StringGetAsync(Key);

            if (cacheResponse.IsNullOrEmpty)
                return null;
            return cacheResponse.ToString();
        }

        public async Task SetCacheAsync(string Key, object response, TimeSpan expireDate)
        {
            if (response == null)
                return;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

               await _database.StringSetAsync(Key,JsonSerializer.Serialize(response),expireDate);

        }
    }
}
