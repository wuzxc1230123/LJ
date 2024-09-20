using LJ.Pack;
using LJ.Test.Host.Web;
using Xunit.Abstractions;

[assembly: TestFramework("LJ.Host.Web.Test.Startup", "LJ.Host.Web.Test")]

namespace LJ.Host.Web.Test
{
    public class Startup(IMessageSink messageSink) : HostTestStartup(messageSink)
    {
        public override void Add(IPackBuilder packBuilder)
        {
        }
    }
}
