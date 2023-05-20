using System;
using System.Collections.Generic;

namespace fullstackCsharp.Models;

public partial class Role
{
    public int IdR { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
