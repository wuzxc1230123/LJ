using AntDesign.ProLayout;
using AntDesign;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.UI.Components;

public partial class FooterContent
{
    public LinkItem[] Links { get; set; } =
    [
        new LinkItem
        {
            Key = "Ant Design Blazor",
            Title = "Ant Design Blazor",
            Href = "https://antblazor.com",
            BlankTarget = true,
        },
        new LinkItem
        {
            Key = "Blazor",
            Title = "Blazor",
            Href = "https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor?WT.mc_id=DT-MVP-5003987",
            BlankTarget = true,
        }
    ];
}
