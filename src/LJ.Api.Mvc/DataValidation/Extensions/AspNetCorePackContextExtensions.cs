using LJ.Api.Mvc.DataValidation.Filters;
using LJ.Api.Mvc.DynamicApi.Providers;
using LJ.AspNetCore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace LJ.Api.Mvc.DataValidation.Extensions
{
    /// <summary>
    /// 服务拓展
    /// </summary>
    public static class AspNetCorePackContextExtensions
    {

        public static IAspNetCorePackContext AddDataValidation(this IAspNetCorePackContext packContext, ApplicationPartManager applicationPartManager)
        {

            //禁用模型验证过滤器
            packContext.ServiceCollection.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = false;
            });

            // 添加控制器特性提供器
            applicationPartManager.FeatureProviders.Add(new DynamicApiControllerFeatureProvider());

            // 配置 Mvc 选项
            packContext.ServiceCollection.AddOptions<MvcOptions>()
               .Configure<IServiceProvider>((options, serviceProvider) =>
               {

                   // 添加全局数据验证
                   // 关闭空引用对象验证
                   options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                   options.Filters.Add<DataValidationFilter>();
               });

            return packContext;
        }

    }
}
