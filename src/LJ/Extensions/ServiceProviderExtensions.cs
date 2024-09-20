using LJ.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Extensions
{
    /// <summary>
    /// 服务拓展
    /// </summary>
    public static class ServiceProviderExtensions
    {

        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns>TOptions</returns>
        public static TOptions GetOptions<TOptions>(this IServiceProvider serviceProvider)
            where TOptions : class, new()
        {
            return serviceProvider.GetRequiredService<IOptionsProvider>().Get<TOptions>();
        }

        /// <summary>
        /// 获取本地化
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IStringLocalizer GetLocalizer(this IServiceProvider serviceProvider,Type type)
        {

            var stringLocalizerType = typeof(IStringLocalizer<>);
            var localizerType = stringLocalizerType.MakeGenericType(type);
            var localizer = serviceProvider.GetRequiredService(localizerType);
            return (localizer as IStringLocalizer)!;
        }

        /// <summary>
        /// 获取本地化
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IStringLocalizer GetLocalizer<T>(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<IStringLocalizer<T>>();
        }


        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static ILogger GetLogger<T>(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<ILogger<T>>();
        }
    }
}
