using System;
using System.Collections.Generic;

namespace TaggerApi.Models;

/// <summary>
/// Contain data about videos
/// </summary>
public partial class Permission
{
    public Guid Token { get; set; }

    public long IdVideo { get; set; }

    public virtual Video IdVideoNavigation { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime Expire {get;set;} 
}