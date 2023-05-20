using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fullstackCsharp.Models;
using System.Text;
using fullstackCsharp.Service;

namespace fullstackCsharp.Controllers
{
    [TypeFilter(typeof(AuthorizeFilter))]
    public class DepartmentsController : Controller
    {
        private readonly ManagerContext _context;

        public DepartmentsController(ManagerContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index(string searchText = "", int pg = 1)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            var department = _context.Departments.ToList();
            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower(); // chuyển tất cả thành chữ thường
                var searchTiengViet = searchText.Normalize(NormalizationForm.FormD);

                department = department.Where(u =>
                    u.Department1.Normalize(NormalizationForm.FormD).ToLower().Contains(searchTiengViet)).ToList();
            }
            const int pageSize = 10;
            if (pg < 1) pg = 1;
            int recsCount = department.Count();
            var paper = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = department.Skip(recSkip).Take(pageSize).ToList();
            this.ViewBag.searchText = searchText;
            this.ViewBag.Pager = paper;
            return View(data);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.IdD == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdD,Department1")] Department department)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdD,Department1")] Department department)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id != department.IdD)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.IdD))
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
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.IdD == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (_context.Departments == null)
            {
                return Problem("Entity set 'ManagerContext.Departments'  is null.");
            }
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return (_context.Departments?.Any(e => e.IdD == id)).GetValueOrDefault();
        }
    }
}
