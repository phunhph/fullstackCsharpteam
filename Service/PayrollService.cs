using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using fullstackCsharp.Models;
using System.Linq;
using fullstackCsharp.Models.ViewModel.Attendances;
using fullstackCsharp.Models.ViewModel.PayOff;
using fullstackCsharp.Models.ViewModel.Salaries;

namespace fullstackCsharp.Service
{
    public class PayrollService : IPayrollService
    {
        private readonly ManagerContext _context;


        public PayrollService(ManagerContext context)
        {
            _context = context;
        }
        //trong attendancecontroller
        public List<MonthlyHoursViewModel> GetMonthlyWorkHours(int? month, int? year)
        {
            var startOfMonth = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var attendanceList = _context.Attendanes
                .Include(a => a.IdUNavigation)
                .Where(a => a.AttendaneDate >= startOfMonth && a.AttendaneDate <= endOfMonth)
                .ToList();

            var usersTotalWorkHours = attendanceList
                .GroupBy(a => a.IdU)
                .Select(g => new
                {
                    UserId = g.Key,
                    FullName = g.First().IdUNavigation.FullName,
                    TotalWorkHours = g.Sum(a => a.WorkHours)
                }).ToList();

            var monthyHoursList = new List<MonthlyHoursViewModel>();
            foreach (var userHours in usersTotalWorkHours)
            {
                monthyHoursList.Add(new MonthlyHoursViewModel
                {
                    UserId = (int)userHours.UserId,
                    FullName = userHours.FullName,
                    TotalWorkHours = (double)userHours.TotalWorkHours
                });
            }

            return monthyHoursList;
        }

        //trong payoffsController
        public List<MonthyPayoffViewModel> GetMonthyPayoffs(int? month, int? year)
        {
            if (!month.HasValue || !year.HasValue)
            {
                var currentDate = DateTime.Now;
                month = currentDate.Month;
                year = currentDate.Year;
            }
            var query = from u in _context.Users
                        join p in _context.Payoffs on u.IdU equals p.IdU into g
                        from pg in g.DefaultIfEmpty()
                        where pg.PayoffDate != null && pg.PayoffDate.Month == month && pg.PayoffDate.Year == year
                        group pg by new { u.IdU, u.FullName } into monthGroup
                        select new MonthyPayoffViewModel
                        {
                            UserId = monthGroup.Key.IdU,
                            FullName = monthGroup.Key.FullName,
                            TotalPayOffMonth = (double)monthGroup.Sum(p => p.Payoff1)
                        };

            return query.ToList();
        }


     /*   public List<TotalSalaryViewModel> GetTotalSalary(int? month, int? year)
        {
            var startOfMonth = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);



            return totalAllSalary.ToList();
        }*/
    }
}
