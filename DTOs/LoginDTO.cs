using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaggerApi.DTOs;

/// <summary>
/// Contain info about users
/// </summary>
public partial class LoginDTO
{
    
    [EmailAddress]
    [StringLength(50, ErrorMessage = "The (Email) value cannot exceed 50 characters. ")]  
    public string Email { get; set; } = null!;

    [StringLength(50, ErrorMessage = "The (Password) value cannot exceed 50 characters. ",MinimumLength = 1)]  
    public string Password { get; set; } = null!;

}