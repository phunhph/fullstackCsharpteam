namespace fullstackCsharp.Models.TableAccessories
{
    public class TotalSalaryViewModel
    {
        public int? IdU { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public double? TotalWorkHours { get; set; }
        public double? TotalPayoff { get; set; }
        public double? BasicSalary { get; set; }
        public double? Coefficient { get; set; }
        public double? AllowanceAmount { get; set; }
        public decimal? TotalSalary { get;set; }
    }
}
