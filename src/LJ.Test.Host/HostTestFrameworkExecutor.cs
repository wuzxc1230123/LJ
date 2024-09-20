using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LJ.Test.Host
{
    /// <summary>
    /// 单元测试框架执行器
    /// </summary>
    public class HostTestFrameworkExecutor(IHost host, AssemblyName assemblyName
            , ISourceInformationProvider sourceInformationProvider
            , IMessageSink diagnosticMessageSink) : XunitTestFrameworkExecutor(assemblyName, sourceInformationProvider, diagnosticMessageSink)
    {
        private readonly IHost _host = host;

        /// <summary>
        /// 执行测试案例
        /// </summary>
        /// <param name="testCases"></param>
        /// <param name="executionMessageSink"></param>
        /// <param name="executionOptions"></param>
        protected async override void RunTestCases(IEnumerable<IXunitTestCase> testCases, IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions)
        {
            await _host.StartAsync();

            using var assemblyRunner = new HostTestAssemblyRunner(_host.Services, TestAssembly, testCases, DiagnosticMessageSink, executionMessageSink, executionOptions);
            await assemblyRunner.RunAsync();

            await _host.StopAsync();

        }
    }
}
