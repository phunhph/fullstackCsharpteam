using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fullstackCsharp.Models;
using fullstackCsharp.Models.ViewModel;
using fullstackCsharp.Service;

namespace fullstackCsharp.Controllers
{
    public class SalariesController : Controller
    {
        private readonly ManagerContext _context;
        private readonly IPayrollService _payrollService;
        public SalariesController(ManagerContext context,IPayrollService payrollService)
        {
            _context = context;
            _payrollService = payrollService;
        }

        public async Task<IActionResult> ListInformationSalary(int? month, int? year)
        {
            var viewModels = _payrollService.GetTotalSalary(month, year);
            ViewBag.Month = month ?? DateTime.Now.Month;
            ViewBag.Year = year ?? DateTime.Now.Year;
            ViewBag.MonthYear = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, 1).ToString("MMMM yyyy");
            //ViewBag.TotalSalaryModel = viewModels;

            return View(viewModels);
        }

        public async Task<IActionResult> AllSalary()
        {
            var allSa = await _context.Users
                        .Join(_context.Salaries, u => u.IdU, s => s.IdU, (u, s) => new { User = u, Salary = s })
                        .Join(_context.Payoffs, us => us.User.IdU, p => p.IdU, (us, p) => new { UserSalary = us, Payoff = p })
                        .Join(_context.Positions, usp => usp.UserSalary.User.IdPosition, pos => pos.IdPosition, (usp, pos) => new { UserSalaryPayoff = usp, Position = pos })
                        .Select(x => new EmployeeViewModel
                        {
                            FullName = x.UserSalaryPayoff.UserSalary.User.FullName,
                            BasicSalary = x.UserSalaryPayoff.UserSalary.Salary.BasicSalary,
                            Payoff1 = x.UserSalaryPayoff.Payoff.Payoff1,
                            Coefficient = x.Position.Coefficient
                        })
                        .ToListAsync();
            return View(allSa);
        }

        // GET: Salaries
        public async Task<IActionResult> Index()
        {
            var managerContext = _context.Salaries.Include(s => s.IdUNavigation);
            return View(await managerContext.ToListAsync());
        }

        // GET: Salaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Salaries == null)
            {
                return NotFound();
            }

            var salary = await _context.Salaries
                .Include(s => s.IdUNavigation)
                .FirstOrDefaultAsync(m => m.IdSalary == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        // GET: Salaries/Create
        public IActionResult Create()
        {
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "IdU");
            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSalary,BasicSalary,StartDate,EndDate,IdU")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "IdU", salary.IdU);
            return View(salary);
        }

        // GET: Salaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Salaries == null)
            {
                return NotFound();
            }

            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null)
            {
                return NotFound();
            }
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "IdU", salary.IdU);
            return View(salary);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSalary,BasicSalary,StartDate,EndDate,IdU")] Salary salary)
        {
            if (id != salary.IdSalary)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaryExists(salary.IdSalary))
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
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "IdU", salary.IdU);
            return View(salary);
        }

        // GET: Salaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Salaries == null)
            {
                return NotFound();
            }

            var salary = await _context.Salaries
                .Include(s => s.IdUNavigation)
                .FirstOrDefaultAsync(m => m.IdSalary == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Salaries == null)
            {
                return Problem("Entity set 'ManagerContext.Salaries'  is null.");
            }
            var salary = await _context.Salaries.FindAsync(id);
            if (salary != null)
            {
                _context.Salaries.Remove(salary);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaryExists(int id)
        {
            return (_context.Salaries?.Any(e => e.IdSalary == id)).GetValueOrDefault();
        }
    }
}
