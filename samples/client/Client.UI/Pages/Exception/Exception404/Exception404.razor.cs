using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.UI.Pages.Exception.Exception404;

public partial class Exception404
{
    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    private void BackHome()
    {
        NavigationManager.NavigateTo("/");
    }
}
