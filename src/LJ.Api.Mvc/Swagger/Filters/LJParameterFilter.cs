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

public class LJParameterFilter : IParameterFilter
{

    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    {
        if (context.PropertyInfo != null)
        {
            ApplyPropertyTags(parameter, context);
        }
        else if (context.ParameterInfo != null)
        {
            ApplyParamTags(parameter, context);
        }
    }

    private static void ApplyPropertyTags(OpenApiParameter parameter, ParameterFilterContext context)
    {
        var propertyMemberName = XmlCommentsNodeNameHelper.GetMemberNameForFieldOrProperty(context.PropertyInfo);
        var propertyNode = SwaggerContext.SelectSingleNode($"/doc/members/member[@name='{propertyMemberName}']");

        if (propertyNode == null) return;

        var summaryNode = propertyNode.SelectSingleNode("summary");
        if (summaryNode != null)
        {
            parameter.Description = XmlCommentsTextHelper.Humanize(summaryNode.InnerXml);
            //parameter.Schema.Description = null; // no need to duplicate

            if (context.PropertyInfo.PropertyType.IsEnum)
            {

                StringBuilder sb = new(parameter.Description);

                sb.AppendLine("<ul>");

                foreach (string enumMemberName in Enum.GetNames(context.PropertyInfo.PropertyType))
                {
                    string fullEnumMemberName = $"F:{context.PropertyInfo.PropertyType.FullName}.{enumMemberName}";
                    long enumValue = Convert.ToInt64(Enum.Parse(context.PropertyInfo.PropertyType, enumMemberName));
                    var typeSummaryNode = SwaggerContext.SelectSingleNode($"/doc/members/member[@name='{fullEnumMemberName}']/summary");
                    if (typeSummaryNode != null)
                    {
                        sb.AppendLine($"<li><b>{enumValue}[{enumMemberName}]</b>: {XmlCommentsTextHelper.Humanize(typeSummaryNode.InnerXml)}</li>");
                    }
                }

                sb.AppendLine("</ul>");
                parameter.Description = sb.ToString();
            }
        }

        var exampleNode = propertyNode.SelectSingleNode("example");
        if (exampleNode == null) return;

        var exampleAsJson = parameter.Schema?.ResolveType(context.SchemaRepository) == "string"
            ? $"\"{exampleNode}\""
            : exampleNode.ToString();

        parameter.Example = OpenApiAnyFactory.CreateFromJson(exampleAsJson);
    }

    private static void ApplyParamTags(OpenApiParameter parameter, ParameterFilterContext context)
    {
        if (context.ParameterInfo.Member is not MethodInfo methodInfo) return;

        var targetMethod = methodInfo.DeclaringType!.IsConstructedGenericType
            ? methodInfo.GetUnderlyingGenericTypeMethod()
            : methodInfo;

        if (targetMethod == null) return;

        var methodMemberName = XmlCommentsNodeNameHelper.GetMemberNameForMethod(targetMethod);
        var paramNode = SwaggerContext.SelectSingleNode(
            $"/doc/members/member[@name='{methodMemberName}']/param[@name='{context.ParameterInfo.Name}']");

        if (paramNode != null)
        {
            parameter.Description = XmlCommentsTextHelper.Humanize(paramNode.InnerXml);

            var example = paramNode.GetAttribute("example", "");
            if (string.IsNullOrEmpty(example)) return;

            var exampleAsJson = parameter.Schema?.ResolveType(context.SchemaRepository) == "string"
                ? $"\"{example}\""
                : example;

            parameter.Example = OpenApiAnyFactory.CreateFromJson(exampleAsJson);
        }
    }
}
