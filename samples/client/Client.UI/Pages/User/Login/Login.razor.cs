using Client.Model.User.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.UI.Pages.User.Login;

public partial class Login
{
    private readonly LoginDto _model = new ();

    [NotNull]
    [Inject] 
    public NavigationManager? NavigationManager { get; set; }

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
