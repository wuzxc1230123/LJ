using AntDesign;
using AntDesign.ProLayout;
using Client.Model.Auth;
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

    [NotNull]
    [Inject]
    private ReuseTabsService? TabService { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    [NotNull]
    [Inject]
    private HttpClient? HttpClient { get; set; }
    /// <summary>
    /// 获得/设置 是否折叠
    /// </summary>
    private bool Collapsed;

    /// <summary>
    /// 获得/设置 是否已授权
    /// </summary>
    private bool IsAuthenticated { get; set; }

    private MenuDataItem[] MenuData = [];

    private CurrentUserDto CurrentUser = new();

    private AvatarMenuItem[] AvatarMenuItems { get; set; } =
          [
              //new() { Key = "center", IconType = "user", Option = L["menu.account.center"]},
              //  new() { Key = "setting", IconType = "setting", Option = L["menu.account.settings"] },
                new() { IsDivider = true },
                new() { Key = "logout", IconType = "logout", Option ="退出登录"}
          ];
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();


        MenuData = await HttpClient.GetFromJsonAsync<MenuDataItem[]>("data/menu.json");
        CurrentUser = await HttpClient.GetFromJsonAsync<CurrentUserDto>("data/current_user.json");

        var url = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
        IsAuthenticated = await OnAuthorizingAsync(url);

    }

    private async Task<bool> OnAuthorizingAsync(string url)
    {
       return await Task.FromResult(true);
    }

    private void HandleSelectUser(MenuItem item)
    {
        switch (item.Key)
        {
            case "logout":
                NavigationManager.NavigateTo("/user/login");
                break;
        }
    }

    private void Reload()
    {
        TabService.ReloadPage();
    }

    private void Toggle()
    {
        Collapsed = !Collapsed;
    }
}
