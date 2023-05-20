using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace fullstackCsharp.Models;

public partial class Payoff
{
    public int IdPay { get; set; }
    [Required]
    public int? IdU { get; set; }

    public decimal? Payoff1 { get; set; }
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]

    public DateTime PayoffDate { get; set; }

    public string? Description { get; set; }

    public virtual User? IdUNavigation { get; set; }
}
