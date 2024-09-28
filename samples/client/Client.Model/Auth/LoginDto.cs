using System.ComponentModel.DataAnnotations;

namespace Client.Model.Auth;

public class LoginDto
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }

    public bool AutoLogin { get; set; }
}
