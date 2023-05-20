using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace fullstackCsharp.Models;

public partial class Allowance
{
    public int IdAllowance { get; set; }

    public int? IdU { get; set; }

    public decimal? AllowanceAmount { get; set; }
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]

    public DateTime? CreateMonth { get; set; }

    public virtual User? IdUNavigation { get; set; }
}
