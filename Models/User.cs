using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fullstackCsharp.Models;

public partial class User
{
    public int IdU { get; set; }

    public string? UserName { get; set; }

    public string? PassWord { get; set; }

    public string? FullName { get; set; }

    public string? Adress { get; set; }

    public int? PhoneNumber { get; set; }

    public string? Gender { get; set; }
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]

    public DateTime? StartDate { get; set; }

    public int? IdD { get; set; }

    public int? IdPosition { get; set; }

    public decimal? IdCard { get; set; }

    public int? Status { get; set; }

    public string? Avatar { get; set; }

    public string? Email { get; set; }
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? Birthday { get; set; }
    public virtual ICollection<Allowance> Allowances { get; set; } = new List<Allowance>();

    public virtual ICollection<Attendane> Attendanes { get; set; } = new List<Attendane>();

    public virtual Department? IdDNavigation { get; set; }

    public virtual Position? IdPositionNavigation { get; set; }

    public virtual ICollection<Payoff> Payoffs { get; set; } = new List<Payoff>();

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    [NotMapped]
    public IFormFile? ImageUpload { get; set; }
}
