using System;
using System.Collections.Generic;

namespace fullstackCsharp.Models;

public partial class T
{
    public int IdA { get; set; }

    public int? IdU { get; set; }

    public string? FullName { get; set; }

    public TimeSpan? Checkin { get; set; }

    public DateTime? AttendaneDate { get; set; }

    public TimeSpan? Checkout { get; set; }

    public decimal? Totalwork { get; set; }
}
