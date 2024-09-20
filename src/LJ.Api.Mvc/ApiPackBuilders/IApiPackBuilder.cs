using LJ.Api.DynamicApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Api.Mvc.ApiPackBuilders
{
    /// <summary>
    /// ApiPack构建器
    /// </summary>
    public interface IApiPackBuilder
    {
        /// <summary>
        /// 添加DynamicApi
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        public IApiPackBuilder AddDynamicApi<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService, IDynamicApi;
    }
}
