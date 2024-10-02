using System;
using System.Collections.Generic;

namespace TaggerApi.Models;

/// <summary>
/// Contain info about users
/// </summary>
public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
