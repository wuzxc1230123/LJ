using LJ.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LJ.Host.Options
{
    public class OptionsManager(IServiceCollection serviceCollection, IConfiguration configuration) : IOptionsManager
    {

        public void Add<TOptions>(string key, string optionsPath) where TOptions : class
        {
            serviceCollection.Configure<TOptions>(
                key,
                configuration.GetSection(optionsPath));
        }

        public void Add<TOptions>(string optionsPath) where TOptions : class
        {
            serviceCollection.Configure<TOptions>(
                configuration.GetSection(optionsPath));
        }
    }
}
