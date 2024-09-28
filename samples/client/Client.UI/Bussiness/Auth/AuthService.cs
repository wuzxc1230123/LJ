using Client.Model.Auth;

namespace Client.UI.Bussiness.Auth;

public class AuthService
{
    private bool isLogin = false;

    public Task LoginAsync()
    {
        isLogin = true;
        return Task.CompletedTask;
    }

    public Task LogoutAsync()
    {
        isLogin = false;
        return Task.CompletedTask;
    }


    public Task<CurrentUserDto?> GetCurrentUserAsync()
    {
        if (isLogin)
        {
            return Task.FromResult<CurrentUserDto?>(new CurrentUserDto()
            {
                Name = "aaaaa",
                Avatar = "https://gw.alipayobjects.com/zos/antfincdn/XAosXuNZyF/BiazfanxmamNRoxxVxka.png"
            });
        }

        return Task.FromResult<CurrentUserDto?>(null);
    }


    public Task<List<MenuDto>> GetMenuAsync()
    {
        return Task.FromResult(new List<MenuDto>()
        {
           new MenuDto()
           {
              Name="Dashboard",
              Key="dashboard",
              Icon="dashboard",
              Path="/dashboard",
              Children=new List<MenuDto>{
                new MenuDto()
                {
                  Name="Home",
                  Key="Home",
                  Path="/",
                }
              }
           }
        });
    }
    public Task<bool> CheckNavigationAsync(string url)
    {
        if (url.Contains("aaaa"))
        {
            return Task.FromResult(false);
        }
        return Task.FromResult(true);
    }

    public bool CheckPermiss(string permiss)
    {
        if (permiss.Contains("aaaa"))
        {
            return false;
        }
        return true;
    }
}
