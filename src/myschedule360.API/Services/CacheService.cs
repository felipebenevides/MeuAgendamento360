using Microsoft.Extensions.Caching.Memory;

namespace myschedule360.API.Services;

public interface ICacheService
{
    T? Get<T>(string key);
    void Set<T>(string key, T value, TimeSpan? expiration = null);
    void Remove(string key);
}

public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public T? Get<T>(string key)
    {
        return _cache.TryGetValue(key, out var value) ? (T?)value : default;
    }

    public void Set<T>(string key, T value, TimeSpan? expiration = null)
    {
        var options = new MemoryCacheEntryOptions();
        if (expiration.HasValue)
            options.SetAbsoluteExpiration(expiration.Value);
        else
            options.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

        _cache.Set(key, value, options);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}