

using LJ.AspNetCore.Service;
using LJ.Dependency;
using LJ.Host.Dependency;
using LJ.Host.Options;
using LJ.Host.Reflection;
using LJ.Options;
using LJ.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LJ.Host.Web.Service
{
    public class AspNetCorePackContext : IAspNetCorePackContext
    {

        public AspNetCorePackContext(WebApplicationBuilder webApplicationBuilder)
        {
            WebApplicationBuilder= webApplicationBuilder;
            ServiceCollection = webApplicationBuilder.Services;
            OptionsManager = new OptionsManager(webApplicationBuilder.Services, webApplicationBuilder.Configuration);
            ReflectionManager = new ReflectionManager();
            DependencyManager = new DependencyManager(webApplicationBuilder.Services, ReflectionManager);
        }

        public WebApplicationBuilder WebApplicationBuilder { get; }


        public IServiceCollection ServiceCollection { get; }

        public IOptionsManager OptionsManager { get; }

        public IReflectionManager ReflectionManager { get; }

        public IDependencyManager DependencyManager { get; }
    }
}
