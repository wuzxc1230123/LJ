using LJ.Api.Mvc.Swagger;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace LJ.Api.Mvc.Swagger.Filters;

public class LJRequestBodyFilter : IRequestBodyFilter
{
    public void Apply(OpenApiRequestBody requestBody, RequestBodyFilterContext context)
    {
        var bodyParameterDescription = context.BodyParameterDescription;

        if (bodyParameterDescription == null) return;

        var propertyInfo = bodyParameterDescription.PropertyInfo();
        if (propertyInfo != null)
        {
            ApplyPropertyTags(requestBody, context, propertyInfo);
            return;
        }

        var parameterInfo = bodyParameterDescription.ParameterInfo();
        if (parameterInfo != null)
        {
            ApplyParamTags(requestBody, context, parameterInfo);
            return;
        }
    }

    private static void ApplyPropertyTags(OpenApiRequestBody requestBody, RequestBodyFilterContext context, PropertyInfo propertyInfo)
    {
        var propertyMemberName = XmlCommentsNodeNameHelper.GetMemberNameForFieldOrProperty(propertyInfo);
        var propertyNode = SwaggerContext.SelectSingleNode($"/doc/members/member[@name='{propertyMemberName}']");

        if (propertyNode == null) return;

        var summaryNode = propertyNode.SelectSingleNode("summary");
        if (summaryNode != null)
            requestBody.Description = XmlCommentsTextHelper.Humanize(summaryNode.InnerXml);

        var exampleNode = propertyNode.SelectSingleNode("example");
        if (exampleNode == null) return;

        foreach (var mediaType in requestBody.Content.Values)
        {
            var exampleAsJson = mediaType.Schema?.ResolveType(context.SchemaRepository) == "string"
                ? $"\"{exampleNode}\""
                : exampleNode.ToString();

            mediaType.Example = OpenApiAnyFactory.CreateFromJson(exampleAsJson);
        }
    }

    private static void ApplyParamTags(OpenApiRequestBody requestBody, RequestBodyFilterContext context, ParameterInfo parameterInfo)
    {
        if (parameterInfo.Member is not MethodInfo methodInfo) return;

        // If method is from a constructed generic type, look for comments from the generic type method
        var targetMethod = methodInfo.DeclaringType!.IsConstructedGenericType
            ? methodInfo.GetUnderlyingGenericTypeMethod()
            : methodInfo;

        if (targetMethod == null) return;

        var methodMemberName = XmlCommentsNodeNameHelper.GetMemberNameForMethod(targetMethod);
        var paramNode = SwaggerContext.SelectSingleNode(
            $"/doc/members/member[@name='{methodMemberName}']/param[@name='{parameterInfo.Name}']");

        if (paramNode != null)
        {
            requestBody.Description = XmlCommentsTextHelper.Humanize(paramNode.InnerXml);

            var example = paramNode.GetAttribute("example", "");
            if (string.IsNullOrEmpty(example)) return;

            foreach (var mediaType in requestBody.Content.Values)
            {
                var exampleAsJson = mediaType.Schema?.ResolveType(context.SchemaRepository) == "string"
                    ? $"\"{example}\""
                    : example;

                mediaType.Example = OpenApiAnyFactory.CreateFromJson(exampleAsJson);
            }
        }
    }
}
