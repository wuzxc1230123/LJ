using LJ.Api.Mvc.Swagger.Filters;
using LJ.AspNetCore.Service;
using LJ.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Xml.XPath;

namespace LJ.Api.Mvc.Swagger.Extensions
{
    /// <summary>
    /// 服务拓展
    /// </summary>
    public static class AspNetCorePackContextExtensions
    {

        public static IAspNetCorePackContext AddSwagger(this IAspNetCorePackContext packContext)
        {
            packContext.ServiceCollection.AddSwaggerGen();

            packContext.OptionsManager.Add<PackSwaggerOptions>("Swagger");
            packContext.ServiceCollection.AddOptions<SwaggerGenOptions>()
                .Configure<IServiceProvider>((options, serviceProvider) =>
                {
                    var packSwaggerOptions = serviceProvider.GetOptions<PackSwaggerOptions>();

                    //Swagger OperationIds
                    options.CustomOperationIds(apiDescription =>
                    {
                        var isMethod = apiDescription.TryGetMethodInfo(out var method);


                        var operationId = apiDescription.RelativePath!.Replace("/", "-")
                                                   .Replace("{", "-")
                                                   .Replace("}", "-") + "-" + apiDescription.HttpMethod!.ToLower().ToUpperCamelCase();

                        return operationId.Replace("--", "-");
                    });

                    //Swagger SchemaId
                    static string DefaultSchemaIdSelector(Type modelType)
                    {
                        var modelName = modelType.Name;

                        // 处理泛型类型问题
                        if (modelType.IsConstructedGenericType)
                        {
                            var prefix = modelType.GetGenericArguments()
                                .Select(genericArg => DefaultSchemaIdSelector(genericArg))
                                .Aggregate((previous, current) => previous + current);

                            // 通过 _ 拼接多个泛型
                            modelName = modelName.Split('`').First() + "_" + prefix;
                        }
                        return modelName;
                    }
                    options.CustomSchemaIds(modelType => DefaultSchemaIdSelector(modelType));


                    var apiDescriptionGroupCollectionProvider = serviceProvider.GetRequiredService<IApiDescriptionGroupCollectionProvider>();
                    foreach (var description in apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items)
                    {
                        options.SwaggerDoc(description.GroupName, null);
                    }

                    var projectXmlComments = packContext.ReflectionManager.GetAssemblies().Select(t => t.GetName()!.Name);
                    foreach (var xmlComment in projectXmlComments)
                    {
                        var assemblyXmlName = xmlComment!.EndsWith(".xml") ? xmlComment : $"{xmlComment}.xml";
                        var assemblyXmlPath = Path.Combine(AppContext.BaseDirectory, assemblyXmlName);

                        if (File.Exists(assemblyXmlPath))
                        {
                            var xmlDoc = new XPathDocument(assemblyXmlPath);
                            SwaggerContext.XPathNavigators.Add(xmlDoc.CreateNavigator());
                        }
                        options.RequestBodyFilter<LJRequestBodyFilter>();
                        options.ParameterFilter<LJParameterFilter>();
                        options.DocumentFilter<LJDocumentFilter>();
                        options.OperationFilter<LJOperationFilter>();
                        options.SchemaFilter<LJSchemaFilter>();

                    }

                    foreach (var security in packSwaggerOptions.Securitys)
                    {
                        //添加Jwt验证设置,添加请求头信息
                        options.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Id = security.Id,
                                        Type = ReferenceType.SecurityScheme
                                    }
                                },
                                new List<string>()
                            }
                        });

                        //放置接口Auth授权按钮
                        options.AddSecurityDefinition(security.Id, new OpenApiSecurityScheme
                        {
                            Description = security.Description,
                            Name = security.Name,//jwt默认的参数名称
                            In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                            Type = SecuritySchemeType.ApiKey
                        });
                    }
                });

            return packContext;
        }

    }
}
