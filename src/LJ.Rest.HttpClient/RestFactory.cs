using LJ.Service;
using Microsoft.Extensions.DependencyInjection;

namespace LJ.Rest.HttpClient
{
    public class RestFactory(IAppProvider appProvider) : IRestFactory
    {
        private readonly IAppProvider _appProvider = appProvider;
        public IRestClient Create()
        {
            return _appProvider.ServiceProvider.GetRequiredService<IRestClient>();
        }
    }
}
