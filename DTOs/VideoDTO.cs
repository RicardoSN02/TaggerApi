using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaggerApi.DTOs;

/// <summary>
/// Contain data about videos
/// </summary>
public partial class VideoDTO
{
    public long Id { get; set; }

    
    [StringLength(50, ErrorMessage = "The (Name) value should be between 1 and 50 characters. ",MinimumLength = 1)]  
    public string Name { get; set; } = null!;

    [Url]
    [StringLength(100, ErrorMessage = "The (Link) value should be between 1 and 100 characters. ",MinimumLength = 1)]  
    public string Link { get; set; } = null!;

    [StringLength(200, ErrorMessage = "The (Description) value should be between 1 and 200 characters. ")]  
    public string? Description { get; set; }


}
