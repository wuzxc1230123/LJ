using LJ.AspNetCore.Service;
using LJ.Pack;
using LJ.Service;
using Microsoft.Extensions.DependencyInjection;

namespace LJ.AspNetCore.Pack
{
    /// <summary>
    /// LJAspNetCorePack 基类
    /// </summary>
    public abstract class LJAspNetCorePack : LJPack
    {
        public override void Add(IPackContext packContext)
        {
        }

        public override Task UseAsync(IPackProvider packProvider)
        {
            return Task.CompletedTask;
        }


        /// <summary>
        /// 加载服务
        /// </summary>
        /// <param name="packContext"></param>
        public abstract void Add(IAspNetCorePackContext packContext);

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="packProvider"></param>
        public abstract Task UseAsync(IAspNetCorePackProvider packProvider);
    }
}
