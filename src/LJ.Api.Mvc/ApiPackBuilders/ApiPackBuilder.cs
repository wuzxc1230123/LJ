using LJ.Api.DynamicApi;
using LJ.Api.Mvc.DynamicApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Api.Mvc.ApiPackBuilders
{
    public class ApiPackBuilder : IApiPackBuilder
    {
        public IApiPackBuilder AddDynamicApi<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService, IDynamicApi
        {
            DynamicApiContext.DynamicApiControllers[typeof(TImplementation)] = typeof(TService);
            return this;
        }
    }
}
