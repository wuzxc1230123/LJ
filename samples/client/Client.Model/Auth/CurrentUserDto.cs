﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.Auth;

public class CurrentUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
}
