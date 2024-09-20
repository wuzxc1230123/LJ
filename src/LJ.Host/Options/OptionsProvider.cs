using Microsoft.Extensions.Options;

namespace LJ.Host.Options
{
    public class OptionsProvider(IServiceProvider serviceProvider) : IOptionsProvider
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public TOptions Get<TOptions>(string key) where TOptions : class
        {
            return _serviceProvider.GetRequiredService<IOptionsMonitor<TOptions>>().Get(key);
        }

        public TOptions Get<TOptions>() where TOptions : class
        {
            return _serviceProvider.GetRequiredService<IOptionsMonitor<TOptions>>().CurrentValue;
        }
    }
}
