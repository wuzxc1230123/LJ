using LJ.Api.Mvc.Friendly.Filters;
using LJ.AspNetCore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace LJ.Api.Mvc.Friendly.Extensions
{
    /// <summary>
    /// 服务拓展
    /// </summary>
    public static class AspNetCorePackContextExtensions
    {

        public static IAspNetCorePackContext AddFriendly(this IAspNetCorePackContext packContext)
        {

            packContext.ServiceCollection.AddOptions<MvcOptions>()
             .Configure<IServiceProvider>((options, serviceProvider) =>
             {
                 ////友好异常拦截
                 options.Filters.Add<FriendlyExceptionFilter>();
             });
            return packContext;
        }

    }
}
