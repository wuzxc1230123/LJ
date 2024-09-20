﻿using LJ.Dependency;
using LJ.Extensions;
using LJ.Host.Service;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LJ.Host.Dependency
{
    public class DependencyManager : IDependencyManager
    {

        public DependencyManager(IServiceCollection serviceCollection, IReflectionManager reflectionManager)
        {
            serviceCollection.AddTransient(typeof(Lazy<>), typeof(Lazier<>));



          
            //查找所有自动注册的服务实现类型进行注册
            var dependencyTypes = FindDependencyTypes(reflectionManager);
            foreach (Type dependencyType in dependencyTypes)
            {
                AddToServices(serviceCollection, dependencyType);
            }
        }

        /// <summary>
        /// 查找所有自动注册的服务实现类型
        /// </summary>
        /// <param name="reflectionManager"></param>
        /// <returns></returns>
        protected virtual List<Type> FindDependencyTypes(IReflectionManager reflectionManager)
        {
            Type[] baseTypes = [typeof(ISingletonDependency), typeof(IScopeDependency), typeof(ITransientDependency)];
            return reflectionManager.GetTypes(type => type.IsClass && !type.IsAbstract && !type.IsInterface
                && !type.HasAttribute<IgnoreDependencyAttribute>()
                && (baseTypes.Any(b => b.IsAssignableFrom(type)) || type.HasAttribute<DependencyAttribute>()));
        }

        /// <summary>
        /// 将服务实现类型注册到服务集合中
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="implementationType">要注册的服务实现类型</param>
        protected virtual void AddToServices(IServiceCollection services, Type implementationType)
        {
            if (implementationType.IsAbstract || implementationType.IsInterface)
            {
                return;
            }
            ServiceLifetime? lifetime = GetLifetimeOrNull(implementationType);
            if (lifetime == null)
            {
                return;
            }
            var dependencyAttribute = implementationType.GetAttribute<DependencyAttribute>();
            if (dependencyAttribute == null)
            {
                return;
            }
            Type[] serviceTypes = GetImplementedInterfaces(implementationType);

            //服务数量为0时注册自身
            if (serviceTypes.Length == 0)
            {
                services.TryAdd(new ServiceDescriptor(implementationType, implementationType, lifetime.Value));
                return;
            }

            //服务实现显示要求注册身处时，注册自身并且继续注册接口
            if (dependencyAttribute.AddSelf == true)
            {
                services.TryAdd(new ServiceDescriptor(implementationType, implementationType, lifetime.Value));
            }

            if (serviceTypes.Length > 1)
            {
                serviceTypes = [.. serviceTypes.OrderBy(m => m.FullName)];
            }

            //注册服务
            for (int i = 0; i < serviceTypes.Length; i++)
            {
                Type serviceType = serviceTypes[i];
                ServiceDescriptor descriptor = new(serviceType, implementationType, lifetime.Value);
                if (lifetime.Value == ServiceLifetime.Transient)
                {
                    services.TryAddEnumerable(descriptor);
                    continue;
                }

                bool multiple = serviceType.HasAttribute<MultipleDependencyAttribute>();
                if (i == 0)
                {
                    if (multiple)
                    {
                        services.Add(descriptor);
                    }
                    else
                    {
                        AddSingleService(services, descriptor, dependencyAttribute);
                    }
                }
                else
                {
                    if (multiple)
                    {
                        services.Add(descriptor);
                    }
                    else
                    {
                        //有多个接口，后边的接口注册使用第一个接口的实例，保证同个实现类的多个接口获得同一实例
                        Type firstServiceType = serviceTypes[0];
                        descriptor = new ServiceDescriptor(serviceType, provider => provider.GetRequiredService(firstServiceType), lifetime.Value);
                        AddSingleService(services, descriptor, dependencyAttribute);
                    }
                }
            }
        }

        /// <summary>
        /// 重写以实现 从类型获取要注册的<see cref="ServiceLifetime"/>生命周期类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>生命周期类型</returns>
        protected virtual ServiceLifetime? GetLifetimeOrNull(Type type)
        {
            var attribute = type.GetAttribute<DependencyAttribute>();
            if (attribute != null)
            {
                return attribute.Lifetime;
            }

            if (type.IsDeriveClassFrom<ITransientDependency>())
            {
                return ServiceLifetime.Transient;
            }

            if (type.IsDeriveClassFrom<IScopeDependency>())
            {
                return ServiceLifetime.Scoped;
            }

            if (type.IsDeriveClassFrom<ISingletonDependency>())
            {
                return ServiceLifetime.Singleton;
            }

            return null;
        }

        /// <summary>
        /// 重写以实现 获取实现类型的所有可注册服务接口
        /// </summary>
        /// <param name="type">依赖注入实现类型</param>
        /// <returns>可注册的服务接口</returns>
        protected virtual Type[] GetImplementedInterfaces(Type type)
        {
            Type[] exceptInterfaces = [typeof(IDisposable)];
            Type[] interfaceTypes = type.GetInterfaces().Where(t => !exceptInterfaces.Contains(t) && !t.HasAttribute<IgnoreDependencyAttribute>()).ToArray();
            for (int index = 0; index < interfaceTypes.Length; index++)
            {
                Type interfaceType = interfaceTypes[index];
                if (interfaceType.IsGenericType && !interfaceType.IsGenericTypeDefinition && interfaceType.FullName == null)
                {
                    interfaceTypes[index] = interfaceType.GetGenericTypeDefinition();
                }
            }
            return interfaceTypes;
        }

        private static void AddSingleService(IServiceCollection services,
            ServiceDescriptor descriptor,
             DependencyAttribute dependencyAttribute)
        {
            if (dependencyAttribute?.ReplaceExisting == true)
            {
                services.Replace(descriptor);
            }
            else if (dependencyAttribute?.TryAdd == true)
            {
                services.TryAdd(descriptor);
            }
            else
            {
                services.Add(descriptor);
            }
        }
    }
}