using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Api.Mvc.DynamicApi.Conventions
{
    /// <summary>
    /// 动态接口控制器应用模型转换器
    /// </summary>
    internal sealed class DynamicApiApplicationModelConvention(IServiceProvider serviceProvider) : IApplicationModelConvention
    {
        private readonly IDynamicApiServiceConvention _dynamicApiApplicationModelConvention = serviceProvider.GetRequiredService<IDynamicApiServiceConvention>();

        public void Apply(ApplicationModel application)
        {
            _dynamicApiApplicationModelConvention.Apply(application);
        }
    }
}
