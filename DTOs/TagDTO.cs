using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaggerApi.DTOs;

/// <summary>
/// Contain video tags
/// </summary>
public partial class TagDTO
{
    public long Id { get; set; }

    [StringLength(200, ErrorMessage = "The (Content) value cannot exceed 200 characters. ",MinimumLength = 1)]  
    public string Content { get; set; } = null!;

    [RegularExpression(@"^(0[0-9]|[0-9]+):([0-5][0-9]):([0-5][0-9])$",ErrorMessage = "Invalid timestamp format {HH:MM:SS}")]
    public string Timestamp { get; set; } = null!;

    [StringLength(50, ErrorMessage = "The (MediaLink) value cannot exceed 50 characters. ")]  
    public string? Medialink { get; set; }

    public long IdVideo { get; set; }

}
