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
using fullstackCsharp.Models.ViewModel.Attendances;
using fullstackCsharp.Service;
using fullstackCsharp.DAO;
using X.PagedList;
using System.Data.SqlClient;
using fullstackCsharp.Models.ViewModel.Attendances;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace fullstackCsharp.Controllers
{
    [TypeFilter(typeof(AuthorizeFilter))]
    public class AttendanesController : Controller
    {
        private readonly ManagerContext _context;
        private readonly IPayrollService _PayrollService;
        private readonly AttendanceDAO _attendanceDAO;

        public AttendanesController(ManagerContext context, IPayrollService payrollService, AttendanceDAO attendanceDAO)
        {
            _context = context;
            _PayrollService = payrollService;
            _attendanceDAO = attendanceDAO;

        }
        public async Task<IActionResult> Index(string searchText = "", int pg = 1)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.ADM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            var managerContext = _context.Attendanes.Include(a => a.IdUNavigation).ToList();
            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower(); // chuyển tất cả thành chữ thường
                var searchTiengViet = searchText.Normalize(NormalizationForm.FormD);

                managerContext = managerContext.Where(u =>
                    u.IdUNavigation.FullName.Normalize(NormalizationForm.FormD).ToLower().Contains(searchTiengViet)).ToList();
            }
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;

            int recsCount = managerContext.Count();

            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = managerContext.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(data);
        }

    /*    public IActionResult Attendancelist( int pg =1)
        {
            //var attendances = _attendanceDAO.SelectAt();
            // AttendanceDAO attendanceDAO = new AttendanceDAO();
            List<AttendancesTotalHours> attendancesTotalHours = _attendanceDAO.SelectAt().ToList();

            const int pageSize  = 10;
            if (pg < 1) 
                pg = 1;

            int recsCount = attendancesTotalHours.Count();

            var pager = new Pager(recsCount,pg, pageSize);
            int recSkip = (pg - 1 ) * pageSize;
            var data = attendancesTotalHours.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            
            return View(data);
        }*/
        //tổng số giờ làm theo tháng của tất cả nhân viên
        public IActionResult TotalWorkMonth(string searchText = "", SearchModel search = null,int pg =1)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.ADM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (search == null)
            {
                search = new SearchModel();
            }
            List<TotalWorkMonth> workMonths = _attendanceDAO.TotalWorkMonths(search).ToList();
            if (!String.IsNullOrEmpty(searchText))
            {
                var searchTiengViet = searchText.Normalize(NormalizationForm.FormD);
                workMonths = workMonths
                    .Where(u => 
                    u.Fullname.Normalize(NormalizationForm.FormD).ToUpper().Contains(searchTiengViet.ToUpper())).ToList();
                if (workMonths.Count == 0)
                {
                    ViewBag.SearchResultMessage = "Không tìm thấy kết quả phù hợp";
                }
            }
            const int pageSize = 10;
            if (pg < 1) pg = 1;
            int remCount = workMonths.Count();
            var pager = new Pager(remCount, pg, pageSize);
            int remSkip = (pg - 1) * pageSize;
            var data = workMonths.Skip(remSkip).Take(pager.PageSize).ToList();
            ViewBag.Pager = pager;
            ViewBag.SearchModel =  search;
            this.ViewBag.searchText = searchText;

            return View(data);
        }

        //tổng giờ làm theo thnags của tài khaonr đăng nhập
        public IActionResult TotalWorkSelf(int? year,int pg=1)
        {
            if (year == null)
            {
                year = DateTime.Now.Year;
            }
            var idu = (int)HttpContext.Session.GetInt32("idu");
            List <TotalWorkHoursSelf> workHoursSelves = _attendanceDAO.TotalWorkHoursSelfs(year,idu).ToList();

            ViewBag.Search =  year;
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;

            int recsCount = workHoursSelves.Count();

            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = workHoursSelves.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
        }
        //thông tin chấm công của tài khoản đăng nhập
        public async Task<IActionResult> AttendanceUser(string searchText = "", int pg=1)
        {
            var idu = HttpContext.Session.GetInt32("idu");


            var managerContext = _context.Attendanes
                                    .Include(a => a.IdUNavigation)
                                    .Where(a => a.IdU == idu)
                                    .ToList();

            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower(); // chuyển tất cả thành chữ thường
                var searchTiengViet = searchText.Normalize(NormalizationForm.FormD);

                managerContext = managerContext.Where(u =>
                    u.IdUNavigation.FullName.Normalize(NormalizationForm.FormD).ToLower().Contains(searchTiengViet)||
                    u.AttendaneDate.ToString().Contains(searchTiengViet)
                    ).ToList();
            }
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;

            int recsCount = managerContext.Count();

            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = managerContext.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            this.ViewBag.searchText = searchText;
            return View(data);
        }

        // GET: Attendanes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.ADM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
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
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.ADM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "FullName");
            return View();
        }

        // POST: Attendanes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Attendane attendane)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.ADM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
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
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.ADM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
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
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.ADM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
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
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.ADM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
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
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.ADM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
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
