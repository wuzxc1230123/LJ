using LJ.Api.DynamicApi;
using LJ.Api.Mvc.DynamicApi;
using LJ.Api.Mvc.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using static System.Collections.Specialized.BitVector32;

namespace LJ.Api.Mvc.Swagger.Filters;

public class LJOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo == null) return;

        // If method is from a constructed generic type, look for comments from the generic type method
        var targetMethod = context.MethodInfo.DeclaringType!.IsConstructedGenericType
            ? context.MethodInfo.GetUnderlyingGenericTypeMethod()
            : context.MethodInfo;

        if (targetMethod == null) return;

        ApplyControllerTags(operation, targetMethod.DeclaringType!);
        ApplyMethodTags(operation, targetMethod);
    }

    private static void ApplyControllerTags(OpenApiOperation operation, Type controllerType)
    {
        var typeMemberName = XmlCommentsNodeNameHelper.GetMemberNameForType(controllerType);
        var responseNodes = SwaggerContext.Select($"/doc/members/member[@name='{typeMemberName}']/response");
        foreach (var responseNode in responseNodes)
        {
            ApplyResponseTags(operation, responseNode);
        }
    }

    private static void ApplyMethodTags(OpenApiOperation operation, MethodInfo methodInfo)
    {
        var methodMemberName = XmlCommentsNodeNameHelper.GetMemberNameForMethod(methodInfo);
        var methodNode = SwaggerContext.SelectSingleNode($"/doc/members/member[@name='{methodMemberName}']");
        if (methodNode == null)
        {
            var methodParameterTypes = methodInfo.GetParameters().Select(p => p.ParameterType).ToArray();
            var interfaceMethodInfo = DynamicApiContext.DynamicApiControllers[methodInfo.DeclaringType!].GetMethod(methodInfo.Name, methodParameterTypes);
            if (interfaceMethodInfo != null)
            {
                methodMemberName = XmlCommentsNodeNameHelper.GetMemberNameForMethod(interfaceMethodInfo);
                methodNode = SwaggerContext.SelectSingleNode($"/doc/members/member[@name='{methodMemberName}']");
            }
        }

        if (methodNode == null) return;

        var summaryNode = methodNode.SelectSingleNode("summary");
        if (summaryNode != null)
            operation.Summary = XmlCommentsTextHelper.Humanize(summaryNode.InnerXml);

        var remarksNode = methodNode.SelectSingleNode("remarks");
        if (remarksNode != null)
            operation.Description = XmlCommentsTextHelper.Humanize(remarksNode.InnerXml);

        var responseNodes = methodNode.Select("response");
        ApplyResponseTags(operation, responseNodes);
    }

    private static void ApplyResponseTags(OpenApiOperation operation, XPathNodeIterator responseNode)
    {
        while (responseNode.MoveNext())
        {
            var code = responseNode.Current!.GetAttribute("code", "");
            var response = operation.Responses.TryGetValue(code, out OpenApiResponse? value) ? value : operation.Responses[code] = new OpenApiResponse();

            response.Description = XmlCommentsTextHelper.Humanize(responseNode.Current.InnerXml);
        }
    }
}
