using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaggerApi.DTOs;

/// <summary>
/// Contain video tags
/// </summary>
public partial class UpdateTagDTO
{

    [StringLength(200, ErrorMessage = "The (Content) value cannot exceed 200 characters. ",MinimumLength = 1)]  
    public string Content { get; set; } = null!;

}