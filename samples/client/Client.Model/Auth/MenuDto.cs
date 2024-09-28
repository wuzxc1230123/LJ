using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.Auth;

public class MenuDto
{
    public string Name { get; set; }=string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;

    public List<MenuDto> Children { get; set; } = [];
}
