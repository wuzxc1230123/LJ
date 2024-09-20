using LJ.AspNetCore.Service;
using LJ.Options;
using LJ.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Host.Web.Service
{
    public class AspNetCorePackProvider(WebApplication webApplication) : IAspNetCorePackProvider
    {

        public WebApplication WebApplication { get; } = webApplication;

        public ILogger Logger { get; } = webApplication.Logger;

        public IOptionsProvider OptionsProvider { get; } = webApplication.Services.GetRequiredService<IOptionsProvider>();

        public IServiceProvider ServiceProvider { get; } = webApplication.Services;
    }
}
