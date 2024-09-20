using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace LJ.Api.Mvc.Swagger;

/// <summary>
/// Swagger上下文
/// </summary>
public static class SwaggerContext
{
    public static ConcurrentBag<XPathNavigator> XPathNavigators { get; set; } = [];

    /// <summary>
    /// 获取Node
    /// </summary>
    /// <param name="xpath"></param>
    /// <returns></returns>
    public static XPathNavigator? SelectSingleNode(string xpath)
    {
        foreach (var xPathNavigator in XPathNavigators)
        {
            var node = xPathNavigator.SelectSingleNode(xpath);
            if (node == null)
            {
                continue;
            }
            return node;
        }
        return default;
    }

    /// <summary>
    /// 获取XPathNodeIterator
    /// </summary>
    /// <param name="xpath"></param>
    /// <returns></returns>
    public static List<XPathNodeIterator> Select(string xpath)
    {
        return XPathNavigators.Select(a => a.Select(xpath)).Where(a => a.Count != 0).ToList();

    }
}
