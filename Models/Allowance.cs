using System;
using System.Collections.Generic;

namespace fullstackCsharp.Models;

public partial class Allowance
{
    public int IdAllowance { get; set; }

    public int? IdU { get; set; }

    public decimal? AllowanceAmount { get; set; }

    public DateTime? CreateMonth { get; set; }

    public virtual User? IdUNavigation { get; set; }
}
