using AntDesign.ProLayout;
using LJ.AspNetCore.Service;
using LJ.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Blazor.AntDesign
{
    public abstract class BlazorAntDesignPack : BlazorPack
    {
        public override void Add(IAspNetCorePackContext packContext)
        {
            packContext.ServiceCollection.AddRazorPages();
            packContext.ServiceCollection.AddServerSideBlazor();
            packContext.ServiceCollection.AddAntDesign();
            packContext.ServiceCollection.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(sp.GetService<NavigationManager>()!.BaseUri)
            });

            packContext.ServiceCollection.PostConfigure<ProSettings>(a =>
            {
                a.NavTheme = "dark";
                a.Layout = "side";
                a.ContentWidth = "Fluid";
                a.FixedHeader = false;
                a.FixSiderbar = true;
                a.Title = "Admin";
                a.PrimaryColor = "daybreak";
                a.ColorWeak = false;
                a.SplitMenus = false;
                a.HeaderRender = true;
                a.MenuRender = true;
                a.MenuHeaderRender = true;
                a.HeaderHeight = 48;
            });
        }

        public override async Task UseAsync(IAspNetCorePackProvider packProvider)
        {
            if (!packProvider.WebApplication.Environment.IsDevelopment())
            {
                packProvider.WebApplication.UseExceptionHandler("/Error");
                packProvider.WebApplication.UseHsts();
            }

            packProvider.WebApplication.UseHttpsRedirection();

            packProvider.WebApplication.UseStaticFiles();

            packProvider.WebApplication.UseRouting();

            packProvider.WebApplication.MapBlazorHub();
            packProvider.WebApplication .MapFallbackToPage("/_Host");


            await Task.CompletedTask;

        }
    }
}
