using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.UI.Components;

public partial class Exception404
{
    [NotNull]
    [Inject] 
    private NavigationManager? NavigationManager { get; set; }

    private void BackHome()
    {
        NavigationManager.NavigateTo("/");
    }
}
