using System;
using System.Collections.Generic;

namespace TaggerApi.Models;

/// <summary>
/// Contain data about videos
/// </summary>
public partial class Video
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Link { get; set; } = null!;

    public string? Description { get; set; }

    public string Permissions { get; set; } = null!;

    public long IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
