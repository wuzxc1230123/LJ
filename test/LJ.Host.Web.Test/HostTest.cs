using LJ.Service;

namespace LJ.Host.Web.Test
{
    public class HostTest(IAppProvider appProvider)
    {
        private readonly IAppProvider _appProvider = appProvider;

        [Fact]
        public void Host_OK()
        {
            Assert.NotNull(_appProvider);
        }
    }
}
