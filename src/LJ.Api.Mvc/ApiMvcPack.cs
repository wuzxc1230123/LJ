using LJ.Api.Mvc.ApiPackBuilders;
using LJ.Api.Mvc.DataValidation.Extensions;
using LJ.Api.Mvc.DynamicApi.Extensions;
using LJ.Api.Mvc.Friendly.Extensions;
using LJ.Api.Mvc.Swagger.Extensions;
using LJ.Api.Mvc.UnifyResult.Extensions;
using LJ.AspNetCore.Service;
using LJ.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace LJ.Api.Mvc;

public abstract class ApiMvcPack : ApiPack
{
  
    public override void Add(IAspNetCorePackContext packContext)
    {

        packContext.ServiceCollection.AddMvc()
            .AddDataAnnotationsLocalization();
        packContext.ServiceCollection.AddEndpointsApiExplorer();


        // 添加配置
        packContext.ServiceCollection.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        });

        var apiPackBuilder = new ApiPackBuilder();
        AddApiPack(apiPackBuilder);

        var partManager = packContext.ServiceCollection.GetSingletonInstance<ApplicationPartManager>();

        // 解决项目类型为 <Project Sdk="Microsoft.NET.Sdk"> 不能加载 API 问题，默认支持 <Project Sdk="Microsoft.NET.Sdk.Web">
        foreach (var assembly in packContext.ReflectionManager.GetAssemblies())
        {
            if (partManager.ApplicationParts.Any(u => u.Name != assembly.GetName().Name))
            {
                partManager.ApplicationParts.Add(new AssemblyPart(assembly));
            }
        }

        packContext.AddSwagger();
        packContext.AddDynamicApi(partManager);
        packContext.AddDataValidation(partManager);
        packContext.AddUnifyResult();
        packContext.AddFriendly();

    }

    public override Task UseAsync(IAspNetCorePackProvider packProvider)
    {

        packProvider.UseSwagger();

        packProvider.WebApplication.UseHttpsRedirection();

        packProvider.WebApplication.UseAuthorization();

        packProvider.WebApplication.MapControllers();

        return Task.CompletedTask;
    }

    public abstract void AddApiPack(IApiPackBuilder apiPackBuilder);
}
