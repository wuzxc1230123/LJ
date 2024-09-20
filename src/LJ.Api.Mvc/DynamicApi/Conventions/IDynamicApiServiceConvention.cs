using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Api.Mvc.DynamicApi.Conventions
{
    /// <summary>
    /// 服务转换
    /// </summary>
    public interface IDynamicApiServiceConvention
    {
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="application"></param>
        void Apply(ApplicationModel application);
    }
}
