using EasyCaching.Core;

namespace LJ.Cache.EasyCaching
{
    public class EasyCachingCache(IEasyCachingProviderFactory easyCachingProviderFactory) : ICache
    {
        private readonly IEasyCachingProviderFactory _easyCachingProviderFactory = easyCachingProviderFactory;


        public async Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellationToken = default) 
        {
            var easyCachingProvider = _easyCachingProviderFactory.GetCachingProvider(Cache.Key);
            await easyCachingProvider.SetAsync(key, value, expiration, cancellationToken);
        }

        public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            var easyCachingProvider = _easyCachingProviderFactory.GetCachingProvider(Cache.Key);
            var cache = await easyCachingProvider.GetAsync<T>(key, cancellationToken);

           return cache.Value;
        }
    }
}
