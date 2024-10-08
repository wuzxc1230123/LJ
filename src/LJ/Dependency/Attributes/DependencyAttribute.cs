﻿using Microsoft.Extensions.DependencyInjection;

namespace LJ.Dependency.Attributes
{
    /// <summary>
    /// 依赖注入行为特性
    /// </summary>
    /// <remarks>
    /// 初始化一个<see cref="DependencyAttribute"/>类型的新实例
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public class DependencyAttribute(ServiceLifetime lifetime) : Attribute
    {
        public ServiceLifetime Lifetime { get; } = lifetime;

        /// <summary>
        /// 获取或设置 是否为TryAdd方式注册，通常用于默认服务，当服务可能被替换时，应设置为true
        /// </summary>
        public bool TryAdd { get; set; }

        /// <summary>
        /// 获取或设置 是否替换已存在的服务实现，通常用于主要服务，当服务存在时即优先使用时，应设置为true
        /// </summary>
        public bool ReplaceExisting { get; set; }

        /// <summary>
        /// 获取或设置 是否注册自身类型，默认没有接口的类型会注册自身，当此属性值为true时，也会注册自身
        /// </summary>
        public bool AddSelf { get; set; }
    }
}
