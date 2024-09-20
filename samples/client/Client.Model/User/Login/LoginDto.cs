﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.User.Login;

public class LoginDto
{
    [Required] 
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }

    public bool AutoLogin { get; set; }
}
