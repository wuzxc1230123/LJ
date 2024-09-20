using LJ.Host.Web;
using Server.Host.Packs;

var hostRunOptions = LJWebHostOptions.Default;
hostRunOptions.ConfigurePack(a =>
{
    a.Add<ApiCorePack>();
});
var host =await WebHost.CreateAsync(hostRunOptions);
host.Run();