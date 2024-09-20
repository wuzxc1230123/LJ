using LJ.Host.Web;
using LJ.Pack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LJ.Test.Host.Web
{
    /// <summary>
    /// 测试配置启动项
    /// </summary>
    /// <remarks>
    /// 构造函数
    /// </remarks>
    /// <param name="messageSink"></param>
    public abstract class HostTestStartup(IMessageSink messageSink) : XunitTestFramework(messageSink)
    {

        /// <summary>
        /// 创建单元测试框架执行器
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
        {

            var hostRunOptions = LJWebHostOptions.Default;
            hostRunOptions.ConfigurePack(a =>
            {
                Add(a);
            });
            var hostTask = WebHost.CreateAsync(hostRunOptions);
            hostTask.Wait();
            return new HostTestFrameworkExecutor(hostTask.Result, assemblyName, SourceInformationProvider, DiagnosticMessageSink);
        }

        /// <summary>
        /// 加载包
        /// </summary>
        /// <param name="packBuilder"></param>
        public abstract void Add(IPackBuilder packBuilder);
    }
}