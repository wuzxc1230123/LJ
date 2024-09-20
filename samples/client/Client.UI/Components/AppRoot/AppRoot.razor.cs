using AntDesign;
using Microsoft.AspNetCore.Components;

namespace Client.UI.Components;

public partial class AppRoot
{
    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }


    private RenderFragment RenderBody() => builder =>
    {
        builder.OpenComponent<ErrorContent>(0);
        builder.AddAttribute(1, nameof(ErrorContent.ChildContent), RenderContent);
        builder.CloseComponent();
    };

    private static RenderFragment RenderComponents() => builder =>
    {
    };

    private RenderFragment RenderContent => builder =>
    {
        builder.AddContent(0, RenderChildContent);
        builder.AddContent(1, RenderComponents());
    };

}
