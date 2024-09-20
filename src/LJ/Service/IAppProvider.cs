using LJ.Thread;
using Microsoft.Extensions.Localization;

namespace LJ.Service
{
    public interface IAppProvider
    {
        /// <summary>
        /// 选项模式提供器
        /// </summary>
        public IOptionsProvider OptionsProvider { get; }

        /// <summary>
        /// 服务提供器
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 异步任务取消标识提供器
        /// </summary>
        public ICancellationTokenProvider CancellationTokenProvider { get; }

        /// <summary>
        ///  获取日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ILogger GetLogger<T>();

        /// <summary>
        ///  获取本地化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IStringLocalizer GetLocalizer<T>();
    }
   
}
