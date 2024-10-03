using System;
using System.Collections.Generic;

namespace TaggerApi.DTOs;

/// <summary>
/// Contain info about users
/// </summary>
public partial class UserDTO
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

}
