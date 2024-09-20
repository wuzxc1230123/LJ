using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Api.Mvc.DynamicApi.Providers
{
    /// <summary>
    /// 动态接口控制器特性提供器
    /// </summary>
    public class DynamicApiControllerFeatureProvider : ControllerFeatureProvider
    {
        /// <summary>
        /// 扫描控制器
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        protected override bool IsController(TypeInfo typeInfo)
        {
            return DynamicApiContext.IsApiController(typeInfo);
        }
    }
}
