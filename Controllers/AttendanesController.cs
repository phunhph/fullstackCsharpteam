using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fullstackCsharp.Models;
using fullstackCsharp.Models.TableAccessories;
using fullstackCsharp.Service;

namespace fullstackCsharp.Controllers
{
    public class AttendanesController : Controller
    {
        private readonly ManagerContext _context;
        private readonly IPayrollService _PayrollService;
        public AttendanesController(ManagerContext context, IPayrollService payrollService)
        {
            _context = context;
            _PayrollService = payrollService;
        }

        //------List số giờ làm của từng nhân viên theo tháng hiện tại -------
        public async Task<IActionResult> TotalWorkMonthNow()
        {
            // Lấy danh sách các bản ghi chấm công trong tháng này
            var attendances = await _context.Attendanes.Include(n => n.IdUNavigation)
                .Where(a => a.AttendaneDate.Month == DateTime.Now.Month)
                .ToListAsync();

            
            var monthlyAttendance = new List<MonthlyAttendanceNow>();
            foreach (var attendance in attendances)
            {
                var totalWorkHours = ((attendance.Checkout ?? TimeSpan.Zero) - (attendance.Checkin ?? TimeSpan.Zero)).TotalHours;
                var monthlyRecord = monthlyAttendance.FirstOrDefault(m => m.UserFullName == attendance.IdUNavigation.FullName);
                if (monthlyRecord != null)
                {
                    monthlyRecord.TotalWorkHours += totalWorkHours;
                }
                else
                {   //thêm vào model view mới
                    monthlyAttendance.Add(new MonthlyAttendanceNow
                    {
                        UserFullName = attendance.IdUNavigation.FullName,
                        TotalWorkHours = totalWorkHours
                    });
                }
            }

            // Trả về kết quả
            return View(monthlyAttendance);
        }

        //--------list giờ làm của từng nhân viên
        public async Task<IActionResult> MonthlyHours(int? month , int? year)
        {

            var monthyHoursList =  _PayrollService.GetMonthlyWorkHours(month,year);
            ViewBag.Month = month ?? DateTime.Now.Month;
            ViewBag.Year = year ?? DateTime.Now.Year;
            ViewBag.MonthYear = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, 1).ToString("MMMM yyyy");
            ViewBag.UsersTotalWorkHours = monthyHoursList;


            return View();
        }
        //thông tin chấm công của tài khoản đăng nhập
        public async Task<IActionResult> AttendanceUser()
        {
            if (!AuthorizationManager.CheckUserLogin(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }
            var idu = HttpContext.Session.GetInt32("idu");


            var managerContext = _context.Attendanes
                                    .Include(a => a.IdUNavigation)
                                    .Where(a=>a.IdU == idu)
                                    .ToList();
            return View(managerContext);
        }

        // GET: Attendanes
        public async Task<IActionResult> Index()
        {
            if (!AuthorizationManager.CheckUserLogin(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }
            var managerContext = _context.Attendanes.Include(a => a.IdUNavigation);
            return View(await managerContext.ToListAsync());
        }

        // GET: Attendanes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!AuthorizationManager.CheckUserLogin(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null || _context.Attendanes == null)
            {
                return NotFound();
            }

            var attendane = await _context.Attendanes
                .Include(a => a.IdUNavigation)
                .FirstOrDefaultAsync(m => m.IdA == id);
            if (attendane == null)
            {
                return NotFound();
            }

            return View(attendane);
        }

        // GET: Attendanes/Create
        public IActionResult Create()
        {
            if (!AuthorizationManager.CheckUserLogin(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "FullName");
            return View();
        }

        // POST: Attendanes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdA,AttendaneDate,Checkin,Checkout,IdU")] Attendane attendane)
        {
            if (!AuthorizationManager.CheckUserLogin(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                _context.Add(attendane);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "FullName", attendane.IdU);
            return View(attendane);
        }

        // GET: Attendanes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!AuthorizationManager.CheckUserLogin(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null || _context.Attendanes == null)
            {
                return NotFound();
            }

            var attendane = await _context.Attendanes.FindAsync(id);
            if (attendane == null)
            {
                return NotFound();
            }
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "FullName", attendane.IdU);
            return View(attendane);
        }

        // POST: Attendanes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdA,AttendaneDate,Checkin,Checkout,IdU")] Attendane attendane)
        {
            if (!AuthorizationManager.CheckUserLogin(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id != attendane.IdA)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendane);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendaneExists(attendane.IdA))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "FullName", attendane.IdU);
            return View(attendane);
        }

        // GET: Attendanes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!AuthorizationManager.CheckUserLogin(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null || _context.Attendanes == null)
            {
                return NotFound();
            }

            var attendane = await _context.Attendanes
                .Include(a => a.IdUNavigation)
                .FirstOrDefaultAsync(m => m.IdA == id);
            if (attendane == null)
            {
                return NotFound();
            }

            return View(attendane);
        }

        // POST: Attendanes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!AuthorizationManager.CheckUserLogin(HttpContext))
            {
                return RedirectToAction("Login", "Account");
            }
            if (_context.Attendanes == null)
            {
                return Problem("Entity set 'ManagerContext.Attendanes'  is null.");
            }
            var attendane = await _context.Attendanes.FindAsync(id);
            if (attendane != null)
            {
                _context.Attendanes.Remove(attendane);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendaneExists(int id)
        {
          return (_context.Attendanes?.Any(e => e.IdA == id)).GetValueOrDefault();
        }
    }
}
