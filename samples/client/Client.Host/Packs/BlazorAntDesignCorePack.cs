using LJ.AspNetCore.Service;
using LJ.Blazor.AntDesign;

namespace Client.Host.Packs;

public class BlazorAntDesignCorePack: BlazorAntDesignPack
{
    public override void Add(IAspNetCorePackContext packContext)
    {
        base.Add(packContext);

        packContext.ServiceCollection.AddAuthorizationCore();
        //packContext.ServiceCollection.AddScoped<ClientAuthenticationStateProvider>();
        //packContext.ServiceCollection.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<ClientAuthenticationStateProvider>());
    }

}
