using LJ.Host.Web.Extensions;
using Microsoft.AspNetCore.Builder;

namespace LJ.Host.Web
{
    public class WebHost
    {
        public static async Task<WebApplication> CreateAsync(LJWebHostOptions hostRunOptions)
        {

            var builder = WebApplication.CreateBuilder(hostRunOptions.Args ?? []);

            //加载模块化
            builder.AddLJ(hostRunOptions.PackBuilderAction);

            var app = builder.Build();
            AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) =>
            {
                app.StopAsync().Wait();
            };
            await app.UseLJAsync();

            return app;
        }
    }
}
