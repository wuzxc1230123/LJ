using EasyCaching.Core.Configurations;
using LJ.Service;
using Microsoft.Extensions.DependencyInjection;

namespace LJ.Cache.EasyCaching.Redis
{
    public class CacheEasyCachingRedisPack: CacheEasyCachingPack
    {
        public override void Add(IPackContext packContext)
        {
            base.Add(packContext);

            packContext.OptionsManager.Add<PackCacheOptions>("Cache");

            packContext.ServiceCollection.AddEasyCaching((a,b) =>
            {
                var cacheOptions = a.GetOptions<PackCacheOptions>();

                b.UseRedis(config =>
                {
                    config.DBConfig.Password = cacheOptions.Password;
                    config.DBConfig.Database = cacheOptions.Database;
                    config.DBConfig.Endpoints.Add(new ServerEndPoint(cacheOptions.Host, cacheOptions.Port));
                }, Cache.Key)
                .WithMessagePack(Cache.Key)
                ;
            });

        }
    }
}
