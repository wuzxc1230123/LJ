using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.UI.Components;

public partial class Redirect : ComponentBase
{
    [Inject]
    [NotNull]
    private NavigationManager? Navigation { get; set; }


    /// <summary>
    /// 获得/设置 登录地址 默认 
    /// </summary>
    [Parameter]
    public string Url { get; set; } = "/User/Login";


    protected override void OnInitialized()
    {
        Navigation.NavigateTo(Url, true);
    }
}
