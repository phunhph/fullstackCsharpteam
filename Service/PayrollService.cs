using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using fullstackCsharp.Models;
using fullstackCsharp.Models.TableAccessories;
using fullstackCsharp.Models.ViewModel;
using System.Linq;

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
                    UserId = userHours.UserId,
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

        public List<TotalSalaryViewModel> GetTotalSalary(int? month, int? year)
        {
            var startOfMonth = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            /*var totalSalary = _context.Users
                            .Include(u => u.IdPositionNavigation)
                            .Join(_context.Payoffs, u => u.IdU, p => p.IdU, (u, p) => new { u, p })
                            .Join(_context.Allowances, up => up.u.IdU, a => a.IdU, (up, a) => new { up.u, up.p, a })
                            .Join(_context.Salaries, upa => upa.u.IdU, s => s.IdU, (upa, s) => new { upa.u, upa.p, upa.a, s })
                            .Join(_context.Attendanes, upas => upas.u.IdU, at => at.IdU, (upas, at) => new { upas.u, upas.p, upas.a, upas.s, at })
                            .Where(x => x.p.PayoffDate >= startOfMonth && x.p.PayoffDate <= endOfMonth
                                        && x.at.AttendaneDate >= startOfMonth && x.at.AttendaneDate <= endOfMonth
                                        && x.a.CreateMonth >= startOfMonth && x.a.CreateMonth <= endOfMonth)
                            .GroupBy(x => x.u.IdU)
                            .Select(g => new 
                            {
                                IdU = g.Key,
                                UserName = g.First().u.UserName,
                                FullName = g.First().u.FullName,
                                AllowanceAmount = (double)g.FirstOrDefault().a.AllowanceAmount,
                                BasicSalary = (double)g.FirstOrDefault().s.BasicSalary,
                                Coefficient = (double)g.FirstOrDefault().u.IdPositionNavigation.Coefficient,
                                TotalPayoff = (double)g.Sum(x => x.p.Payoff1),
                                TotalWorkHours = (double)g.Sum(h => h.at.WorkHours != null ? TimeSpan.Parse(h.at.WorkHours).TotalHours : 0),
                            }).ToList();*/

            /*  var attendanceList = _context.Attendanes
                  .Include(a => a.IdUNavigation)
                  .Where(a => a.AttendaneDate >= startOfMonth && a.AttendaneDate <= endOfMonth)
                  .ToList();*/
            // query lấy dữ liệu từ nhiều bảng để tính lương


            var totalSalary =
                from u in _context.Users
                .Include(u => u.IdPositionNavigation)
                join p in _context.Payoffs.Where(p=> (p.PayoffDate >= startOfMonth && p.PayoffDate <=endOfMonth ) || p ==null )
                on u.IdU equals p.IdU into j1
                from p in j1.DefaultIfEmpty()

                join a in _context.Allowances.Where(a=> (a.CreateMonth >= startOfMonth && a.CreateMonth <= endOfMonth) || a==null)
                on u.IdU equals a.IdU into j2
                from a in j2.DefaultIfEmpty()

                join s in _context.Salaries.Where(s=> s == null || s.StartDate >= startOfMonth)
                on u.IdU equals s.IdU into j3
                from s in j3.DefaultIfEmpty()

                join at in _context.Attendanes.Where( at=> (at.AttendaneDate.Month >= startOfMonth.Month && at.AttendaneDate.Month <= endOfMonth.Month) || at == null)
                on u.IdU equals at.IdU into j4
                from at in j4.DefaultIfEmpty()
                /*where ( s==null ||  s.StartDate >= startOfMonth ) && (u == null || u.StartDate >= startOfMonth)
                   && (at == null || ( at.AttendaneDate.Month >= startOfMonth.Month && at.AttendaneDate.Month <= endOfMonth.Month ))
                 && (a == null || ( a.CreateMonth >= startOfMonth && a.CreateMonth <= endOfMonth ))
                && (p == null || (p.PayoffDate.Month >= startOfMonth.Month && p.PayoffDate.Month <= endOfMonth.Month))*/
                group new { at, u, a, s, p } by u.IdU into g
                select new
                {
                    IdU = g.Key,
                    UserName = g.First().u.UserName,
                    FullName = g.First().u.FullName,
                    AllowanceAmount = (double)(g.First(x => x.a != null).a.AllowanceAmount ?? 0),
                    BasicSalary = (double)(g.FirstOrDefault(x => x.s != null).s.BasicSalary ?? 0),
                    Coefficient = (double)(g.First().u.IdPositionNavigation.Coefficient ?? 0),
                    TotalPayoff = (double)g.Sum(x => x.p.Payoff1),
                    //TotalWorkHours = (double)(g.FirstOrDefault(x => x.at != null).at.WorkHours ?? 0)
                    TotalWorkHours = (double)g.Sum(w=> w.at.WorkHours),

                };

            /* var totalSalary = from u in _context.Users.Include(u => u.IdPositionNavigation)
                               join s in _context.Salaries.Where(s => s.StartDate >= startOfMonth && s.StartDate <= endOfMonth)
                               on u.IdU equals s.IdU
                               join pay in _context.Payoffs.Where(p => p.PayoffDate >= startOfMonth && p.PayoffDate <= endOfMonth)
                               on s.IdU equals pay.IdU
                               join a in _context.Allowances.Where(a => a.CreateMonth >= startOfMonth && a.CreateMonth <= endOfMonth)
                               on pay.IdU equals a.IdU
                               join at in _context.Attendanes.Where(at => at.AttendaneDate >= startOfMonth && at.AttendaneDate <= endOfMonth)
                               on a.IdU equals at.IdU into joined
                               from allSalary in joined.DefaultIfEmpty()
                               group new { u, s, pay, a, allSalary }
                               by new
                               {
                                   u.IdU, u.FullName, u.UserName, s.BasicSalary, a.AllowanceAmount, u.IdPositionNavigation

                               } into g
                               select new 
                               {
                                   IdU = g.Key.IdU,
                                   UserName = g.Key.FullName,
                                   FullName = g.Key.FullName,
                                   BasicSalary = (double?)g.Key.BasicSalary,
                                   AllowanceAmount = (double?)g.Key.AllowanceAmount,
                                   Coefficient = (double?)g.Key.IdPositionNavigation.Coefficient,
                                   TotalPayoff = (double?)g.Sum(p => (p.pay.Payoff1 == null ? 0 : p.pay.Payoff1)),
                                   TotalWorkHours =(double)g.Sum(w => w.allSalary.WorkHours == null ? 0 : w.allSalary.WorkHours)
                               };*/

            var totalAllSalary = new List<TotalSalaryViewModel>();


            foreach (var s in totalSalary)
            {
                totalAllSalary.Add(new TotalSalaryViewModel
                {
                    IdU = s.IdU,
                    UserName = s.UserName,
                    FullName = s.FullName,
                    BasicSalary = s.BasicSalary,
                    Coefficient = s.Coefficient,
                    TotalPayoff = s.TotalPayoff,
                   TotalWorkHours =s.TotalWorkHours,
                    AllowanceAmount = s.AllowanceAmount,
                    TotalSalary = (decimal)(
                    ((s.BasicSalary * s.Coefficient) + s.AllowanceAmount / 200) * s.TotalWorkHours + s.TotalPayoff)
                });
            }

            /*var userList = _context.Users.Include(p=>p.IdPositionNavigation)
                .GroupBy(a=>a.IdU)
                .Select(s => new TotalSalaryViewModel
                {
                    UserName = s.First().UserName,
                    FullName = s.First().FullName,
                    Coefficient = (double)s.First().IdPositionNavigation.Coefficient
                })
                .ToList();
            var attendanceList = _context.Attendanes.Include(a => a.IdUNavigation)
                .Where(a => a.AttendaneDate >= startOfMonth && a.AttendaneDate <= endOfMonth)
                .GroupBy(a => a.IdU)
                .Select(s => new TotalSalaryViewModel
                {
                    TotalWorkHours = s.Sum(a => a.WorkHours != null ? TimeSpan.Parse(a.WorkHours).TotalHours : 0),
                }).ToList();*/



            return totalAllSalary.ToList();
        }
    }
}
