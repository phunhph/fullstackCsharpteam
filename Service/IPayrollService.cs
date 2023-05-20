using fullstackCsharp.Models.ViewModel.Attendances;
using fullstackCsharp.Models.ViewModel.PayOff;
using fullstackCsharp.Models.ViewModel.Salaries;

namespace fullstackCsharp.Service
{
    public interface IPayrollService
    {
        List<MonthlyHoursViewModel> GetMonthlyWorkHours(int? month, int? year);
        List<MonthyPayoffViewModel> GetMonthyPayoffs(int? month, int? year);
       // List<TotalSalaryViewModel> GetTotalSalary(int? month, int? year);
    }
}
