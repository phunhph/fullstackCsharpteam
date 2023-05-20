using System;
using System.Collections.Generic;

namespace fullstackCsharp.Models;

public partial class Position
{
    public int IdPosition { get; set; }

    public string? Position1 { get; set; }

    public decimal? Coefficient { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
