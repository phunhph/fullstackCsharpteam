using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fullstackCsharp.Models;
using fullstackCsharp.Service;
using System.Text;

namespace fullstackCsharp.Controllers
{
    [TypeFilter(typeof(AuthorizeFilter))]
    public class PayoffsController : Controller
    {
        private readonly ManagerContext _context;
        private readonly IPayrollService _payrollService;

        public PayoffsController(ManagerContext context,IPayrollService payrollService)
        {
            _context = context;
            _payrollService = payrollService;
        }

        //tính tổng thưởng phạt theo tháng
        public IActionResult MonthlyPayoffs(int? month , int? year,int pg = 1)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            var viewModel = _payrollService.GetMonthyPayoffs(month, year).ToList();

            ViewBag.Month = month ?? DateTime.Now.Month;
            ViewBag.Year = year ?? DateTime.Now.Year;
            ViewBag.MonthYear = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, 1).ToString("MMMM yyyy");
            //ViewBag.us = monthyHoursList;
           
            const int pageSize = 10;
            if (pg < 1) pg = 1;
            int recsCount = viewModel.Count();
            var paper = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = viewModel.Skip(recSkip).Take(pageSize).ToList();
            this.ViewBag.Pager = paper;

            return View(data);
        }


        // GET: Payoffs
        public async Task<IActionResult> Index(string searchText = "", int pg = 1)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            var managerContext = _context.Payoffs.Include(p => p.IdUNavigation).ToList();
            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower(); // chuyển tất cả thành chữ thường
                var searchTiengViet = searchText.Normalize(NormalizationForm.FormD);

                managerContext = managerContext.Where(u =>
                    u.IdUNavigation.FullName.Normalize(NormalizationForm.FormD).ToLower().Contains(searchTiengViet) ||
                    u.Description.Normalize(NormalizationForm.FormD).ToLower().Contains(searchTiengViet)
                    ).ToList();
            }
            const int pageSize = 10;
            if (pg < 1) pg = 1;
            int recsCount = managerContext.Count();
            var paper = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = managerContext.Skip(recSkip).Take(pageSize).ToList();
            this.ViewBag.Pager = paper;
            return View(data);
        }

        // GET: Payoffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.Payoffs == null)
            {
                return NotFound();
            }

            var payoff = await _context.Payoffs
                .Include(p => p.IdUNavigation)
                .FirstOrDefaultAsync(m => m.IdPay == id);
            if (payoff == null)
            {
                return NotFound();
            }

            return View(payoff);
        }

        // GET: Payoffs/Create
        public IActionResult Create()
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "FullName");
            return View();
        }

        // POST: Payoffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Payoff payoff)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(payoff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "FullName", payoff.IdU);
            return View(payoff);
        }

        // GET: Payoffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.Payoffs == null)
            {
                return NotFound();
            }

            var payoff = await _context.Payoffs.FindAsync(id);
            if (payoff == null)
            {
                return NotFound();
            }
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "FullName", payoff.IdU);
            return View(payoff);
        }

        // POST: Payoffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Payoff payoff)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id != payoff.IdPay)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payoff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayoffExists(payoff.IdPay))
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
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "FullName", payoff.IdU);
            return View(payoff);
        }

        // GET: Payoffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.Payoffs == null)
            {
                return NotFound();
            }

            var payoff = await _context.Payoffs
                .Include(p => p.IdUNavigation)
                .FirstOrDefaultAsync(m => m.IdPay == id);
            if (payoff == null)
            {
                return NotFound();
            }

            return View(payoff);
        }

        // POST: Payoffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (_context.Payoffs == null)
            {
                return Problem("Entity set 'ManagerContext.Payoffs'  is null.");
            }
            var payoff = await _context.Payoffs.FindAsync(id);
            if (payoff != null)
            {
                _context.Payoffs.Remove(payoff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayoffExists(int id)
        {
          return (_context.Payoffs?.Any(e => e.IdPay == id)).GetValueOrDefault();
        }
    }
}
