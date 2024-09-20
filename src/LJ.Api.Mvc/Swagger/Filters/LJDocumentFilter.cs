using LJ.Api.DynamicApi;
using LJ.Api.Mvc.DynamicApi;
using LJ.Api.Mvc.Swagger;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Any;
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

public class LJDocumentFilter : IDocumentFilter
{
    private const string MemberXPath = "/doc/members/member[@name='{0}']";
    private const string SummaryTag = "summary";



    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {

        var controllerNamesAndTypes = context.ApiDescriptions
            .Select(apiDesc => apiDesc.ActionDescriptor as ControllerActionDescriptor)
            .Where(actionDesc => actionDesc != null)
            .GroupBy(actionDesc => actionDesc!.ControllerName)
            .Select(group => new KeyValuePair<string, Type>(group.Key, group.First()!.ControllerTypeInfo.AsType()));

        foreach (var nameAndType in controllerNamesAndTypes)
        {
            var memberName = XmlCommentsNodeNameHelper.GetMemberNameForType(nameAndType.Value);
            var typeNode = SwaggerContext.SelectSingleNode(string.Format(MemberXPath, memberName));
            if (typeNode == null)
            {
                var dynamicApiType = DynamicApiContext.DynamicApiControllers[nameAndType.Value];
                memberName = XmlCommentsNodeNameHelper.GetMemberNameForType(dynamicApiType);
                typeNode = SwaggerContext.SelectSingleNode(string.Format(MemberXPath, memberName));
            }

            if (typeNode != null)
            {
                var summaryNode = typeNode.SelectSingleNode(SummaryTag);
                if (summaryNode != null)
                {
                    swaggerDoc.Tags ??= [];

                    swaggerDoc.Tags.Add(new OpenApiTag
                    {
                        Name = nameAndType.Key,
                        Description = XmlCommentsTextHelper.Humanize(summaryNode.InnerXml)
                    });
                }
            }
        }


    }
}
