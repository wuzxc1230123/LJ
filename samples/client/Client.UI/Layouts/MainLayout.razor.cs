using AntDesign;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.UI.Layouts;

public partial class MainLayout
{
    private bool Collapsed;
    private MenuDataItem[] MenuData = [];

    [NotNull]
    [Inject] 
    private ReuseTabsService? TabService { get; set; }

    [NotNull]
    [Inject] 
    private HttpClient? HttpClient { get; set; }


    protected override async Task OnInitializedAsync()
    {
        MenuData = await HttpClient.GetFromJsonAsync<MenuDataItem[]>("data/menu.json");
    }

    void Reload()
    {
        TabService.ReloadPage();
    }

    void Toggle()
    {
        Collapsed = !Collapsed;
    }
}
