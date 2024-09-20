using LJ.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Cache.EasyCaching
{
    public class CacheEasyCachingPack : CachePack
    {
        public override void Add(IPackContext packContext)
        {
            packContext.ServiceCollection.AddTransient<ICache, EasyCachingCache>();
        }

        public override Task UseAsync(IPackProvider packProvider)
        {
            return Task.CompletedTask;  
        }
    }
}
