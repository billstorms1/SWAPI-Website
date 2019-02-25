using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;


namespace SWAPI.Helpers
{

    /// <summary>
    /// To do. This isn't much better than simply implementing IMemoryCache. Need to refactor to accept generic types.
    /// </summary>
    public interface ICache
    {
        List<JObject> CheckCache(string cacheKey);
        void SetCache(List<JObject> resultsList, string cacheKey);

    }
    public class Cache : ICache
    {
        private static IMemoryCache _cache;
        public static List<JObject> Names;

        public Cache(IMemoryCache cache)
        {
            _cache = cache;
        }
       
        public List<JObject> CheckCache(string cacheKey)
        {
            _cache.TryGetValue(cacheKey, out Names);
            return Names;
        }

        public void SetCache(List<JObject> resultsList, string cacheKey)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1));
            _cache.Set(cacheKey, resultsList, cacheEntryOptions);
        }
    }
}