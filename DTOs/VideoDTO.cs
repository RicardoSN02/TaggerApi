using System;
using System.Collections.Generic;

namespace TaggerApi.DTOs;

/// <summary>
/// Contain data about videos
/// </summary>
public partial class VideoDTO
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Link { get; set; } = null!;

    public string? Description { get; set; }

    public string Permissions { get; set; } = null!;

    public long IdUser { get; set; }

}
