using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.UI.Components;

public  class ErrorContent: ErrorBoundaryBase
{

    [Inject]
    [NotNull]
    private IErrorBoundaryLogger? ErrorBoundaryLogger { get; set; }


    [NotNull]
    [Inject]
    private MessageService? MessageService { get; set; }


    protected override void OnInitialized()
    {
        base.OnInitialized();

        MaximumErrorCount = 1;
    }
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        Recover();
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<ErrorContent>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<ErrorContent>.Value), this);
        builder.AddAttribute(2, nameof(CascadingValue<ErrorContent>.IsFixed), true);

        var content = ChildContent;
        builder.AddAttribute(3, nameof(CascadingValue<ErrorContent>.ChildContent), content);
        builder.CloseComponent();
    }

    protected override async Task OnErrorAsync(Exception ex)
    {
        await ErrorBoundaryLogger.LogErrorAsync(ex);
        await MessageService.Error(ex.Message);
    }
}
