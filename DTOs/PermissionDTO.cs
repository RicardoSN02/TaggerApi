using System;
using System.Collections.Generic;

namespace TaggerApi.DTOs;

public partial class PermissionDTO{

    public string Token { get; set; }

    public long IdVideo { get; set; }

    public string Role { get; set; } = null!;

    public DateTime Expire {get;set;}  

}