using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LJ.Test.Host
{
    /// <summary>
    /// 单元测试多个测试实例调用器
    /// </summary>
    public class HostTestCollectionRunner(IServiceProvider serviceProvider, Dictionary<Type, object> assemblyFixtureMappings,
                                                        ITestCollection testCollection,
                                                        IEnumerable<IXunitTestCase> testCases,
                                                        IMessageSink diagnosticMessageSink,
                                                        IMessageBus messageBus,
                                                        ITestCaseOrderer testCaseOrderer,
                                                        ExceptionAggregator aggregator,
                                                        CancellationTokenSource cancellationTokenSource) : XunitTestCollectionRunner(testCollection, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource)
    {
        private readonly Dictionary<Type, object> _assemblyFixtureMappings = assemblyFixtureMappings;
        private readonly IMessageSink _diagnosticMessageSink = diagnosticMessageSink;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        /// <summary>
        /// 创建服务作用域
        /// </summary>
        private IServiceScope? serviceScope;

        /// <summary>
        /// 单元测试实例测试时触发
        /// </summary>
        /// <param name="testClass"></param>
        /// <param name="class"></param>
        /// <param name="testCases"></param>
        /// <returns></returns>
        protected override Task<RunSummary> RunTestClassAsync(ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<IXunitTestCase> testCases)
        {
            // 定义单元测试实例所有构造函数参数实例集合
            var combinedFixtures = new Dictionary<Type, object>(_assemblyFixtureMappings);
            foreach (var kvp in CollectionFixtureMappings)
            {
                combinedFixtures[kvp.Key] = kvp.Value;
            }

            // 获取测试实例构造函数
            var constructors = @class.Type.GetConstructors();

            // 不允许多个构造函数
            if (constructors.Length > 1) throw new InvalidProgramException("More than one constructor declaration found.");

            // 如果声明了构造函数
            if (constructors.Length > 0)
            {
                // 获取构造函数参数
                var parameters = constructors[0]
                    .GetParameters()
                    .Where(u => !u.ParameterType.Assembly.GetName().Name!.StartsWith("xunit."));

                // 创建服务作用域
                serviceScope = _serviceProvider.CreateScope();

                // 循环所有接口参数并进行服务解析
                foreach (var parameter in parameters)
                {
                    var serviceType = parameter.ParameterType;
                    object serviceInstance = serviceScope.ServiceProvider.GetRequiredService(serviceType);


                    combinedFixtures.TryAdd(serviceType, serviceInstance);
                }
            }

            // 创建单元测试实例
            return new XunitTestClassRunner(testClass, @class, testCases, _diagnosticMessageSink, MessageBus, TestCaseOrderer, new ExceptionAggregator(Aggregator), CancellationTokenSource, combinedFixtures).RunAsync();
        }

        /// <summary>
        /// 单元测试实例销毁时触发
        /// </summary>
        /// <returns></returns>
        protected override Task BeforeTestCollectionFinishedAsync()
        {
            serviceScope?.Dispose();
            return base.BeforeTestCollectionFinishedAsync();
        }
    }
}
