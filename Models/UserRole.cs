using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;

namespace fullstackCsharp.Models;

public partial class UserRole
{
    public int IdUr { get; set; }

    [Required]
    public int IdU { get; set; }

    public int IdR { get; set; }

    public virtual Role? IdRNavigation { get; set; } = null!;

    public virtual User? IdUNavigation { get; set; } = null!;
}
