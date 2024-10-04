using System;
using System.Collections.Generic;

namespace TaggerApi.Models;

/// <summary>
/// Contain video tags
/// </summary>
public partial class Tag
{
    public long Id { get; set; }

    public string Content { get; set; } = null!;

    public string Timestamp { get; set; } = null!;

    public string? Medialink { get; set; }

    public string IdUser { get; set; }

    public long IdVideo { get; set; }

    public virtual Video IdVideoNavigation { get; set; } = null!;
}
