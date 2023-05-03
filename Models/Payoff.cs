using System;
using System.Collections.Generic;

namespace fullstackCsharp.Models;

public partial class Payoff
{
    public int IdPay { get; set; }

    public int? IdU { get; set; }

    public decimal Payoff1 { get; set; }

    public DateTime PayoffDate { get; set; }

    public string Description { get; set; }

    public virtual User? IdUNavigation { get; set; }
}
