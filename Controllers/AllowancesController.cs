using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fullstackCsharp.Models;

namespace fullstackCsharp.Controllers
{
    public class AllowancesController : Controller
    {
        private readonly ManagerContext _context;

        public AllowancesController(ManagerContext context)
        {
            _context = context;
        }

        // GET: Allowances
        public async Task<IActionResult> Index()
        {
            var managerContext = _context.Allowances.Include(a => a.IdUNavigation);
            return View(await managerContext.ToListAsync());
        }

        // GET: Allowances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Allowances == null)
            {
                return NotFound();
            }

            var allowance = await _context.Allowances
                .Include(a => a.IdUNavigation)
                .FirstOrDefaultAsync(m => m.IdAllowance == id);
            if (allowance == null)
            {
                return NotFound();
            }

            return View(allowance);
        }

        // GET: Allowances/Create
        public IActionResult Create()
        {
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "IdU");
            return View();
        }

        // POST: Allowances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAllowance,IdU,AllowanceAmount,CreateMonth")] Allowance allowance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allowance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "IdU", allowance.IdU);
            return View(allowance);
        }

        // GET: Allowances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Allowances == null)
            {
                return NotFound();
            }

            var allowance = await _context.Allowances.FindAsync(id);
            if (allowance == null)
            {
                return NotFound();
            }
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "IdU", allowance.IdU);
            return View(allowance);
        }

        // POST: Allowances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAllowance,IdU,AllowanceAmount,CreateMonth")] Allowance allowance)
        {
            if (id != allowance.IdAllowance)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allowance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllowanceExists(allowance.IdAllowance))
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
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "IdU", allowance.IdU);
            return View(allowance);
        }

        // GET: Allowances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Allowances == null)
            {
                return NotFound();
            }

            var allowance = await _context.Allowances
                .Include(a => a.IdUNavigation)
                .FirstOrDefaultAsync(m => m.IdAllowance == id);
            if (allowance == null)
            {
                return NotFound();
            }

            return View(allowance);
        }

        // POST: Allowances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Allowances == null)
            {
                return Problem("Entity set 'ManagerContext.Allowances'  is null.");
            }
            var allowance = await _context.Allowances.FindAsync(id);
            if (allowance != null)
            {
                _context.Allowances.Remove(allowance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllowanceExists(int id)
        {
          return (_context.Allowances?.Any(e => e.IdAllowance == id)).GetValueOrDefault();
        }
    }
}
