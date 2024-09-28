using Client.Model.Auth;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace Client.UI.Pages.User.Login;

public partial class Login
{
    private readonly LoginDto _model = new ();

    [NotNull]
    [Inject] 
    public NavigationManager? NavigationManager { get; set; }

    //[Inject]
    //public ClientAuthenticationStateProvider? AuthStateProvider { get; set; }

    public void HandleSubmit()
    {
        if (_model.UserName == "admin" && _model.Password == "ant.design")
        {
            NavigationManager.NavigateTo("/");
            return;
        }
        //throw new NotImplementedException();

        if (_model.UserName == "user" && _model.Password == "ant.design") NavigationManager.NavigateTo("/");
    }
}
