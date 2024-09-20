using AntDesign;
using AntDesign.ProLayout;
using Client.Model.User.CurrentUser;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.UI.Components;

public partial class RightContent: AntDomComponentBase
{
    private CurrentUser CurrentUser = new ();

    private AvatarMenuItem[] AvatarMenuItems { get; set; } =
          [
              //new() { Key = "center", IconType = "user", Option = L["menu.account.center"]},
              //  new() { Key = "setting", IconType = "setting", Option = L["menu.account.settings"] },
              //  new() { IsDivider = true },
                new() { Key = "logout", IconType = "logout", Option ="退出登录"}
          ];


    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }


    [NotNull]
    [Inject]
    private HttpClient? HttpClient { get; set; }


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        SetClassMap();
        CurrentUser = await HttpClient.GetFromJsonAsync<CurrentUser>("data/current_user.json");

    }

    private void SetClassMap()
    {
        ClassMapper
            .Clear()
            .Add("right");
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
}
