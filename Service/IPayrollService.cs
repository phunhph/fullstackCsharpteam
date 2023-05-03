using fullstackCsharp.Models.TableAccessories;
using fullstackCsharp.Models.ViewModel;

namespace fullstackCsharp.Service
{
    public interface IPayrollService
    {
        List<MonthlyHoursViewModel> GetMonthlyWorkHours(int? month, int? year);
        List<MonthyPayoffViewModel> GetMonthyPayoffs(int? month, int? year);
        List<TotalSalaryViewModel> GetTotalSalary(int? month, int? year);
    }
}
