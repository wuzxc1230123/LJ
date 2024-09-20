using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Host.Dependency
{
    /// <summary>
    /// Lazy延迟加载解析器
    /// </summary>
    /// <remarks>
    /// 初始化一个<see cref="Lazier{T}"/>类型的新实例
    /// </remarks>
    internal class Lazier<T>(IServiceProvider provider) : Lazy<T>(provider.GetRequiredService<T>) where T : class
    {
    }
}
