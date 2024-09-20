using LJ.Api.Mvc.UnifyResult.Filters;
using LJ.Api.Mvc.UnifyResult.Providers;
using LJ.AspNetCore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace LJ.Api.Mvc.UnifyResult.Extensions
{
    /// <summary>
    /// 服务拓展
    /// </summary>
    public static class AspNetCorePackContextExtensions
    {

        public static IAspNetCorePackContext AddUnifyResult(this IAspNetCorePackContext packContext)
        {
            packContext.ServiceCollection.AddTransient<IUnifyResultProvider, UnifyResultProvider>();

            packContext.ServiceCollection.AddOptions<MvcOptions>()
             .Configure<IServiceProvider>((options, serviceProvider) =>
             {
                 // 添加成功规范化结果筛选器
                 options.Filters.Add<SucceededUnifyResultFilter>();
             });
            return packContext;
        }

    }
}
