namespace fullstackCsharp.Models.ViewModel
{
    public class EmployeeViewModel
    {
        public string FullName { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? Payoff1 { get; set; }
        public decimal? Coefficient { get; set; }
        public int IdU { get; internal set; }
        public decimal Payoff { get; internal set; }
        public decimal? EstimatedSalary { get; internal set; }
    }
}
