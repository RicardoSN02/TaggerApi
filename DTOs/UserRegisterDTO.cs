using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaggerApi.DTOs;

/// <summary>
/// Contain info about users
/// </summary>
public partial class UserRegisterDTO
{
    [EmailAddress]
    [StringLength(50, ErrorMessage = "The (Email) value cannot exceed 50 characters. ")]  
    public string Email { get; set; } = null!;

    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$
    ", ErrorMessage = "Password requirements unfulfilled") ]
    [StringLength(50, ErrorMessage = "The (Password) value cannot exceed 50 characters. ")]  
    public string Password { get; set; } = null!;

}