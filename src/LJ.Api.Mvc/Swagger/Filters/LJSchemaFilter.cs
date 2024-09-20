using LJ.Api.Mvc.Swagger;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using System.Xml.XPath;

namespace LJ.Api.Mvc.Swagger.Filters;

public class LJSchemaFilter : ISchemaFilter
{


    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        ApplyTypeTags(schema, context.Type);

        if (context.MemberInfo != null)
        {
            ApplyMemberTags(schema, context);
        }

        if (context.Type.IsEnum)
        {
            ApplyEnum(schema, context.Type);
        }
    }

    private static void ApplyTypeTags(OpenApiSchema schema, Type type)
    {
        var typeMemberName = XmlCommentsNodeNameHelper.GetMemberNameForType(type);
        var typeSummaryNode = SwaggerContext.SelectSingleNode($"/doc/members/member[@name='{typeMemberName}']/summary");

        if (typeSummaryNode != null)
        {
            schema.Description = XmlCommentsTextHelper.Humanize(typeSummaryNode.InnerXml);
        }
    }

    private static void ApplyMemberTags(OpenApiSchema schema, SchemaFilterContext context)
    {
        var fieldOrPropertyMemberName = XmlCommentsNodeNameHelper.GetMemberNameForFieldOrProperty(context.MemberInfo);
        var fieldOrPropertyNode = SwaggerContext.SelectSingleNode($"/doc/members/member[@name='{fieldOrPropertyMemberName}']");

        if (fieldOrPropertyNode == null) return;

        var summaryNode = fieldOrPropertyNode.SelectSingleNode("summary");
        if (summaryNode != null)
            schema.Description = XmlCommentsTextHelper.Humanize(summaryNode.InnerXml);
        var exampleNode = fieldOrPropertyNode.SelectSingleNode("example");
        if (exampleNode != null)
        {
            var exampleAsJson = schema.ResolveType(context.SchemaRepository) == "string" && !exampleNode.Value.Equals("null")
                ? $"\"{exampleNode}\""
                : exampleNode.ToString();

            schema.Example = OpenApiAnyFactory.CreateFromJson(exampleAsJson);
        }
    }


    private static void ApplyEnum(OpenApiSchema schema, Type enumType)
    {
        StringBuilder sb = new(schema.Description);

        sb.AppendLine("<ul>");

        foreach (string enumMemberName in Enum.GetNames(enumType))
        {

            string fullEnumMemberName = $"F:{enumType.FullName}.{enumMemberName}";
            long enumValue = Convert.ToInt64(Enum.Parse(enumType, enumMemberName));
            var typeSummaryNode = SwaggerContext.SelectSingleNode($"/doc/members/member[@name='{fullEnumMemberName}']/summary");
            if (typeSummaryNode != null)
            {
                sb.AppendLine($"<li><b>{enumValue}[{enumMemberName}]</b>: {XmlCommentsTextHelper.Humanize(typeSummaryNode.InnerXml)}</li>");
            }
        }

        sb.AppendLine("</ul>");

        schema.Description = sb.ToString();
    }
}