using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace fullstackCsharp.Models;

public partial class Salary
{
    public int IdSalary { get; set; }

    public decimal? BasicSalary { get; set; }

    public int? IdU { get; set; }
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]

    public DateTime? ChangeDate { get; set; }

    public string? Describe { get; set; }

    public virtual User? IdUNavigation { get; set; }
}
