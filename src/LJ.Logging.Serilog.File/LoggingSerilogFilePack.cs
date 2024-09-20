using LJ.AspNetCore.Service;
using Serilog;
using Serilog.Events;

namespace LJ.Logging.Serilog.File
{
    public abstract class LoggingSerilogFilePack : LoggingSerilogPack
    {
        public override void Add(IAspNetCorePackContext packContext)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
             .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
             .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
             .Enrich.FromLogContext()
             .WriteTo.Async(c => c.File($"Logs/{DateTime.Now:yyyy-MM-dd}-log.txt"))
             .CreateLogger();

            packContext.ServiceCollection.AddSerilog();

        }


        public override Task UseAsync(IAspNetCorePackProvider packProvider)
        {
            packProvider.WebApplication.Lifetime.ApplicationStopped.Register(() =>
            {
                //清除日志
                Log.CloseAndFlush();
            });
            return  Task.CompletedTask; 
        }
    }
}
