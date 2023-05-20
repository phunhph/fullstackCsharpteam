using System;
using System.Collections.Generic;

namespace fullstackCsharp.Models;

public partial class Department
{
    public int IdD { get; set; }

    public string? Department1 { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
