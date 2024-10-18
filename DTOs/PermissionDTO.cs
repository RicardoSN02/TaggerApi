using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaggerApi.DTOs;

public partial class PermissionDTO{

    [StringLength(100, ErrorMessage = "The (Token) value cannot exceed 100 characters.")]  
    public string Token { get; set; }

    public long IdVideo { get; set; }

    [RegularExpression(@"^((V|v)iewer|((E|e)ditor))$",ErrorMessage = "Invalid Role")]
    public string Role { get; set; } = null!;

    public DateTime Expire {get;set;}  

}