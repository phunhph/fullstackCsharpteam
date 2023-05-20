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
    public class UserRolesController : Controller
    {
        private readonly ManagerContext _context;

        public UserRolesController(ManagerContext context)
        {
            _context = context;
        }

        // GET: UserRoles


        public async Task<IActionResult> Index(string searchText = "",int pg=1)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin, (int)RoleNameId.EM
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            var managerContext = _context.UserRoles.Include(u => u.IdRNavigation).Include(u => u.IdUNavigation).ToList();
            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower(); // chuyển tất cả thành chữ thường
                var searchTiengViet = searchText.Normalize(NormalizationForm.FormD);
                managerContext = managerContext.Where(u =>
                    u.IdUNavigation.FullName.Normalize(NormalizationForm.FormD).ToLower().Contains(searchTiengViet) ||
                    u.IdRNavigation.RoleName.Normalize(NormalizationForm.FormD).ToLower().Contains(searchTiengViet)
                ).ToList();
                // Kiểm tra số lượng kết quả tìm kiếm
                if (managerContext.Count == 0)
                {
                    ViewBag.SearchResultMessage = "Không tìm thấy kết quả phù hợp";
                }
            }
            const int pageSize = 10;
            if (pg < 1) pg = 1;
            int recsCount = managerContext.Count();
            var paper = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = managerContext.Skip(recSkip).Take(pageSize).ToList();
            this.ViewBag.Pager = paper;
            this.ViewBag.searchText = searchText;
            return View(data);
        }

        // GET: UserRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.UserRoles == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRoles
                .Include(u => u.IdRNavigation)
                .Include(u => u.IdUNavigation)
                .FirstOrDefaultAsync(m => m.IdUr == id);
            if (userRole == null)
            {
                return NotFound();
            }

            return View(userRole);
        }

        // GET: UserRoles/Create
        public IActionResult Create()
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            ViewData["IdR"] = new SelectList(_context.Roles, "IdR", "RoleName");
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "FullName");
            return View();
        }

        // POST: UserRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( UserRole userRole)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            ViewData["IdR"] = new SelectList(_context.Roles, "IdR", "RoleName", userRole.IdR);
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "FullName", userRole.IdU);
            if (ModelState.IsValid)
            {
                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userRole);
        }

        // GET: UserRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.UserRoles == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole == null)
            {
                return NotFound();
            }
            ViewData["IdR"] = new SelectList(_context.Roles, "IdR", "IdR", userRole.IdR);
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "IdU", userRole.IdU);
            return View(userRole);
        }

        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUr,IdU,IdR")] UserRole userRole)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id != userRole.IdUr)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRoleExists(userRole.IdUr))
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
            ViewData["IdR"] = new SelectList(_context.Roles, "IdR", "IdR", userRole.IdR);
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "IdU", userRole.IdU);
            return View(userRole);
        }

        // GET: UserRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.UserRoles == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRoles
                .Include(u => u.IdRNavigation)
                .Include(u => u.IdUNavigation)
                .FirstOrDefaultAsync(m => m.IdUr == id);
            if (userRole == null)
            {
                return NotFound();
            }

            return View(userRole);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (_context.UserRoles == null)
            {
                return Problem("Entity set 'ManagerContext.UserRoles'  is null.");
            }
            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRoleExists(int id)
        {
          return (_context.UserRoles?.Any(e => e.IdUr == id)).GetValueOrDefault();
        }
    }
}
