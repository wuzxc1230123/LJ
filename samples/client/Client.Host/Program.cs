using Client.Host.Packs;
using LJ.Host.Web;

var hostRunOptions = LJWebHostOptions.Default;
hostRunOptions.ConfigurePack(a =>
{
    a.Add<BlazorAntDesignCorePack>();
});
var host = await WebHost.CreateAsync(hostRunOptions);
host.Run();