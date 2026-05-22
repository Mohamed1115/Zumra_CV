using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Zumra.Application.Services.Redis;

public class RedisCacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }
    public async Task<T?> GetCachedData<T>(string key)
    {
        var jsonData =await _cache.GetStringAsync(key);

        if (jsonData == null)
            return default(T);

        return  JsonSerializer.Deserialize<T>(jsonData)!;
    }
    
    public async Task<bool> SetCachedData<T>(string key, T data, TimeSpan cacheDuration)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = cacheDuration
        };

        var jsonData = JsonSerializer.Serialize(data);
        await _cache.SetStringAsync(key, jsonData, options);
        return true;
    }
}