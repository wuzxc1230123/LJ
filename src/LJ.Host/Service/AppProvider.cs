using LJ.Extensions;
using LJ.Options;
using LJ.Service;
using LJ.Thread;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace LJ.Host.Service
{
    public class AppProvider(IServiceProvider serviceProvider) : IAppProvider
    {
        private readonly Lazy<IOptionsProvider> _optionsProvider = serviceProvider.GetRequiredService<Lazy<IOptionsProvider>>();

        private readonly Lazy<ICancellationTokenProvider> _cancellationTokenProvider = serviceProvider.GetRequiredService<Lazy<ICancellationTokenProvider>>();

        public IOptionsProvider OptionsProvider => _optionsProvider.Value;

        public IServiceProvider ServiceProvider => serviceProvider;

        public ICancellationTokenProvider CancellationTokenProvider => _cancellationTokenProvider.Value;

        public IStringLocalizer  GetLocalizer<T>()
        {
            return ServiceProvider.GetLocalizer<T>();
        }

        public ILogger GetLogger<T>()
        {
           return ServiceProvider.GetLogger<T>();
        }
    }
}
