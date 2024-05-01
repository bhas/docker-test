using Application.Utilities;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Cache;
public class Cache(IMemoryCache cache) : ICache
{
    public void Remove(string key)
    {
        cache.Remove(key);
    }

    public void Set<T>(string key, T value, TimeSpan duration)
    {
        cache.Set(key, value, duration);
    }

    public bool TryGetValue<T>(string key, out T? cacheValue)
    {
        return cache.TryGetValue(key, out cacheValue);
    }
}
