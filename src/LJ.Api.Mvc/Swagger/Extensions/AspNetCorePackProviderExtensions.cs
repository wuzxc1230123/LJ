using LJ.Api.Mvc.Swagger.Extensions;
using LJ.AspNetCore.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace LJ.Api.Mvc.Swagger.Extensions
{
    /// <summary>
    /// 服务拓展
    /// </summary>
    public static class AspNetCorePackProviderExtensions
    {

        public static IAspNetCorePackProvider UseSwagger(this IAspNetCorePackProvider packProvider)
        {
            packProvider.WebApplication.UseSwagger();
            packProvider.WebApplication.UseSwaggerUI(options =>
            {
                var apiDescriptionGroupCollectionProvider = packProvider.ServiceProvider.GetRequiredService<IApiDescriptionGroupCollectionProvider>();
                foreach (var description in apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
                }
            });

            return packProvider;
        }

    }
}
