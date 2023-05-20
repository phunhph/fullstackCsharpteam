using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fullstackCsharp.Models;
using fullstackCsharp.Service;

namespace fullstackCsharp.Controllers
{
    public class PositionsController : Controller
    {
        private readonly ManagerContext _context;

        public PositionsController(ManagerContext context)
        {
            _context = context;
        }

        // GET: Positions
        public async Task<IActionResult> Index()
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin, (int)RoleNameId.EM
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            return _context.Positions != null ? 
                          View(await _context.Positions.ToListAsync()) :
                          Problem("Entity set 'ManagerContext.Positions'  is null.");
        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin, (int)RoleNameId.EM
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
                .FirstOrDefaultAsync(m => m.IdPosition == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // GET: Positions/Create
        public IActionResult Create()
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin, (int)RoleNameId.EM
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Position position)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin, (int)RoleNameId.EM
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(position);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        // GET: Positions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin, (int)RoleNameId.EM
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }
            return View(position);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPosition,Position1,Coefficient")] Position position)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin, (int)RoleNameId.EM
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id != position.IdPosition)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(position);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionExists(position.IdPosition))
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
            return View(position);
        }

        // GET: Positions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin, (int)RoleNameId.EM
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
                .FirstOrDefaultAsync(m => m.IdPosition == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin, (int)RoleNameId.EM
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (_context.Positions == null)
            {
                return Problem("Entity set 'ManagerContext.Positions'  is null.");
            }
            var position = await _context.Positions.FindAsync(id);
            if (position != null)
            {
                _context.Positions.Remove(position);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PositionExists(int id)
        {
          return (_context.Positions?.Any(e => e.IdPosition == id)).GetValueOrDefault();
        }
    }
}
