using LJ.AspNetCore.Pack;
using LJ.Dependency;
using LJ.Extensions;
using LJ.Host.Localizer;
using LJ.Host.Options;
using LJ.Host.Pack;
using LJ.Host.Service;
using LJ.Host.Web.Dependency;
using LJ.Host.Web.Service;
using LJ.Host.Web.Thread;
using LJ.Localizer;
using LJ.Options;
using LJ.Pack;
using LJ.Service;
using LJ.Thread;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace LJ.Host.Web.Extensions
{
    public static class LJExtensions
    {
        /// <summary>
        /// 加载LJ 模块
        /// </summary>
        /// <param name="webApplicationBuilder"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static WebApplicationBuilder AddLJ(this WebApplicationBuilder webApplicationBuilder, Action<IPackBuilder>? builderAction = null)
        {

            webApplicationBuilder.Services.AddHttpContextAccessor();
            webApplicationBuilder.Services.AddTransient<ICancellationTokenProvider, HttpContextCancellationTokenProvider>();
            webApplicationBuilder.Services.TryAddSingleton<IHybridServiceScopeFactory, HttpContextServiceScopeFactory>();

            webApplicationBuilder.Services.AddTransient<IOptionsProvider, OptionsProvider>();
            webApplicationBuilder.Services.AddTransient<IAppProvider, AppProvider>();

            webApplicationBuilder.Services.AddScoped<ILocalizationService, LocalizationService>();
            webApplicationBuilder.Services.AddScoped(typeof(IStringLocalizer<>), typeof(LJStringLocalizer<>));
            var packContext = new AspNetCorePackContext(webApplicationBuilder);

            #region 本地化
            packContext.OptionsManager.Add<PackLocalizationOptions>("Localization");

            packContext.ServiceCollection.AddLocalization();

            packContext.ServiceCollection.AddOptions<RequestLocalizationOptions>()
               .Configure<IServiceProvider>((options, serviceProvider) =>
               {
                   var packLocalizationOptions = serviceProvider.GetOptions<PackLocalizationOptions>();
                   var supportedCultures = packLocalizationOptions.Cultures.Select(a => new CultureInfo(a)).ToList();

                   options.DefaultRequestCulture = new RequestCulture(
                       culture: packLocalizationOptions.DefaultCulture,
                       uiCulture: packLocalizationOptions.DefaultCulture);
                   options.SupportedCultures = supportedCultures;
                   options.SupportedUICultures = supportedCultures;

               });
            #endregion


            var packBuilder = new PackBuilder();
            builderAction?.Invoke(packBuilder);

            packBuilder.Load();


            foreach (var pack in packBuilder.LoadPacks)
            {
                pack.Add(packContext);
                if (pack is LJAspNetCorePack aspNetCorePack)
                {
                    aspNetCorePack.Add(packContext);
                }
            }
            webApplicationBuilder.Services.AddSingleton<IPackBuilder>(packBuilder);

            return webApplicationBuilder;
        }

        /// <summary>
        /// 启动LJ模块
        /// </summary>
        /// <param name="webApplication"></param>
        /// <returns></returns>
        public static async Task<WebApplication> UseLJAsync(this WebApplication webApplication)
        {
            var packProvider = new AspNetCorePackProvider(webApplication);

            var packBuilder = webApplication.Services.GetRequiredService<IPackBuilder>();

            #region 本地化
            packProvider.WebApplication.UseRequestLocalization();
            #endregion


            packProvider.Logger.LogInformation("---------------------");
            packProvider.Logger.LogInformation("Load LJ");

        
            foreach (var pack in packBuilder.LoadPacks)
            {
              await  pack.UseAsync(packProvider);

                if (pack is LJAspNetCorePack aspNetCorePack)
                {
                  await  aspNetCorePack.UseAsync(packProvider);
                }
                packProvider.Logger.LogInformation("Load Pack:{name}", pack.GetType().Name);
            }
            packProvider.Logger.LogInformation("Load Pack Ok1");
            packProvider.Logger.LogInformation("---------------------");
            return webApplication;
        }
    }
}
