using System;
using System.Collections.Generic;

namespace TaggerApi.DTOs;

/// <summary>
/// Contain info about users
/// </summary>
public partial class LoginDTO
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

}