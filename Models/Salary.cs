using System;
using System.Collections.Generic;

namespace fullstackCsharp.Models;

public partial class Salary
{
    public int IdSalary { get; set; }

    public decimal? BasicSalary { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? IdU { get; set; }

    public virtual User? IdUNavigation { get; set; }
}
