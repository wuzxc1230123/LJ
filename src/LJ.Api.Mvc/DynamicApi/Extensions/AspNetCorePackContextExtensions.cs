using LJ.Api.DynamicApi.Conventions;
using LJ.Api.Mvc.DynamicApi.Conventions;
using LJ.Api.Mvc.DynamicApi.Providers;
using LJ.AspNetCore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace LJ.Api.Mvc.DynamicApi.Extensions
{
    /// <summary>
    /// 服务拓展
    /// </summary>
    public static class AspNetCorePackContextExtensions
    {

        public static IAspNetCorePackContext AddDynamicApi(this IAspNetCorePackContext packContext, ApplicationPartManager applicationPartManager)
        {
            packContext.OptionsManager.Add<PackDynamicApiOptions>("DynamicApi");
            packContext.ServiceCollection.AddTransient<IDynamicApiServiceConvention, DynamicApiServiceConvention>();
            // 添加控制器特性提供器
            applicationPartManager.FeatureProviders.Add(new DynamicApiControllerFeatureProvider());
            // 配置 Mvc 选项
            packContext.ServiceCollection.AddOptions<MvcOptions>()
               .Configure<IServiceProvider>((options, serviceProvider) =>
               {
                   options.Conventions.Add(new GroupNameConvention());
                   // 添加应用模型转换器
                   options.Conventions.Add(new DynamicApiApplicationModelConvention(serviceProvider));

               });

            return packContext;
        }

    }
}
