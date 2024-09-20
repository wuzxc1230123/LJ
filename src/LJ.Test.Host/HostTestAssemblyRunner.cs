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
    /// 单元测试程序集调用器
    /// </summary>
    /// <remarks>
    /// 构造函数
    /// </remarks>
    /// <param name="testAssembly"></param>
    /// <param name="testCases"></param>
    /// <param name="diagnosticMessageSink"></param>
    /// <param name="executionMessageSink"></param>
    /// <param name="executionOptions"></param>
    public class HostTestAssemblyRunner(IServiceProvider serviceProvider, ITestAssembly testAssembly,
                                                      IEnumerable<IXunitTestCase> testCases,
                                                      IMessageSink diagnosticMessageSink,
                                                      IMessageSink executionMessageSink,
                                                      ITestFrameworkExecutionOptions executionOptions) : XunitTestAssemblyRunner(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions)
    {
        private readonly Dictionary<Type, object> _assemblyFixtureMappings = [];
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        /// <summary>
        /// 单元测试程序集调用时触发
        /// </summary>
        /// <returns></returns>
        protected async override Task AfterTestAssemblyStartingAsync()
        {
            // 让测试程序集回归初始状态
            await base.AfterTestAssemblyStartingAsync();

        }

        /// <summary>
        /// 单元测试程序集销毁时触发
        /// </summary>
        /// <returns></returns>
        protected override Task BeforeTestAssemblyFinishedAsync()
        {
            // Make sure we clean up everybody who is disposable, and use Aggregator.Run to isolate Dispose failures
            foreach (var disposable in _assemblyFixtureMappings.Values.OfType<IDisposable>())
                Aggregator.Run(disposable.Dispose);

            return base.BeforeTestAssemblyFinishedAsync();
        }

        /// <summary>
        /// 执行多个测试实例
        /// </summary>
        /// <param name="messageBus"></param>
        /// <param name="testCollection"></param>
        /// <param name="testCases"></param>
        /// <param name="cancellationTokenSource"></param>
        /// <returns></returns>
        protected override Task<RunSummary> RunTestCollectionAsync(IMessageBus messageBus,
                                                                   ITestCollection testCollection,
                                                                   IEnumerable<IXunitTestCase> testCases,
                                                                   CancellationTokenSource cancellationTokenSource)
        {
            return new HostTestCollectionRunner(_serviceProvider, _assemblyFixtureMappings, testCollection, testCases, DiagnosticMessageSink, messageBus, TestCaseOrderer, new ExceptionAggregator(Aggregator), cancellationTokenSource).RunAsync();
        }
    }
}
