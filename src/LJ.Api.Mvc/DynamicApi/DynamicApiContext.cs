using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Api.Mvc.DynamicApi
{
    /// <summary>
    ///  DynamicApi 上下文
    /// </summary>
    public static class DynamicApiContext
    {
        /// <summary>
        /// Api控制器
        /// </summary>
        public static Dictionary<Type, Type> DynamicApiControllers { get; } = [];

        /// <summary>
        /// Api控制器判定缓存
        /// </summary>
        public static ConcurrentDictionary<Type, bool> IsApiControllerCached { get; } = [];

        /// <summary>
        /// Api路由缓存
        /// </summary>
        public static Dictionary<string, string> Apis { get; } = [];
        /// <summary>
        /// 是否是Api控制器
        /// </summary>
        /// <param name="type">type</param>
        /// <returns></returns>
        public static bool IsApiController(Type type)
        {
            return IsApiControllerCached.GetOrAdd(type, Function);

            // 本地静态方法
            static bool Function(Type type)
            {
                return DynamicApiControllers.Keys.Any(a => a == type); ;
            }
        }
    }
}
