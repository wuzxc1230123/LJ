using LJ.Service;
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
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static T GetSingletonInstance<T>(this IServiceCollection services)
        {
            var service = services.GetSingletonInstanceOrNull<T>();
            return service == null
                ? throw new InvalidOperationException("Could not find singleton service: " + typeof(T).AssemblyQualifiedName)
                : service;
        }
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T? GetSingletonInstanceOrNull<T>(this IServiceCollection services)
        {

            return (T?)services
                .FirstOrDefault(d => d.ServiceType == typeof(T))
                ?.NormalizedImplementationInstance();
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static object? NormalizedImplementationInstance(this ServiceDescriptor descriptor)
        {
            return descriptor.IsKeyedService ? descriptor.KeyedImplementationInstance : descriptor.ImplementationInstance;
        }

    }
}
