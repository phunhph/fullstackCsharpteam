using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fullstackCsharp.Models;
using fullstackCsharp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using X.PagedList;
using System.Globalization;
using System.Text;

namespace fullstackCsharp.Controllers
{
    [TypeFilter(typeof(AuthorizeFilter))]
    public class UsersController : Controller
    {
        private readonly ManagerContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UsersController(ManagerContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        //profile
        public async Task<IActionResult> Profile(int? id)
        {
            if(HttpContext.Session.GetInt32("idu") != id)
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users
                .Include(u=>u.IdDNavigation)
                .Include(u=>u.IdPositionNavigation)
                .Include(u=>u.UserRoles)
                .FirstOrDefaultAsync(m=>m.IdU == id);
            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        public async Task<IActionResult> EditProfile(int? id)
        {
            if (HttpContext.Session.GetInt32("idu") != id)
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            ViewData["IdPosition"] = new SelectList(_context.Positions, "IdPosition", "Position1");
            ViewData["IdD"] = new SelectList(_context.Departments, "IdD", "Department1");
            return View(user);
        }

        // POST: Users/Editprofile/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(int? id, User user)
        {
            if (HttpContext.Session.GetInt32("idu") != id)
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id != user.IdU)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var oldUser = await _context.Users.FindAsync(id);

                    if (oldUser == null)
                    {
                        return NotFound();
                    }
                    if (user.ImageUpload != null && user.ImageUpload.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(user.Avatar))
                        {
                            string oldAvatarPath = Path.Combine(_hostingEnvironment.WebRootPath, user.Avatar.TrimStart('/'));
                            if (System.IO.File.Exists(oldAvatarPath))
                            {
                                System.IO.File.Delete(oldAvatarPath);
                            }
                        }
                        // lưu ảnh mới
                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/Avatar");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + user.ImageUpload.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var filesStream = new FileStream(filePath, FileMode.Create))
                        {
                            await user.ImageUpload.CopyToAsync(filesStream);
                        }
                        user.Avatar = "/images/Avatar/" + uniqueFileName;
                    }
                    else
                    {
                        user.Avatar = user.Avatar; // giữ nguyên ảnh cũ nếu không có ảnh mới
                    }
                    _context.Entry(oldUser).CurrentValues.SetValues(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Profile","Users");

                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.IdU))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction("HomeHome","Home");

            }

            ViewData["IdPosition"] = new SelectList(_context.Positions, "IdPosition", "Position1");
            ViewData["IdD"] = new SelectList(_context.Departments, "IdD", "Department1");

            return View(user);
        }

        // GET: Users
        public async Task<IActionResult> Index(int pg=1,string searchText="")
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.EM,(int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            
            var users = _context.Users
                .Include(u=>u.IdDNavigation)
                .Include (u=>u.IdPositionNavigation)
                .ToList();

            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower(); // chuyển tất cả thành chữ thường
                var searchTiengViet = searchText.Normalize(NormalizationForm.FormD);
                users = users.Where(u =>
                    u.FullName.Normalize(NormalizationForm.FormD).ToLower().Contains(searchTiengViet) ||
                    u.Email.Normalize(NormalizationForm.FormD).ToLower().Contains(searchTiengViet)
                ).ToList();
                // Kiểm tra số lượng kết quả tìm kiếm
                if (users.Count == 0)
                {
                    ViewBag.SearchResultMessage = "Không tìm thấy kết quả phù hợp";
                }
            }
            const int pageSize = 10;
            if(pg<1) pg =1;
            int recsCount = users.Count();
            var paper = new Pager(recsCount,pg,pageSize);
            int recSkip = (pg-1) * pageSize;
            var data = users.Skip(recSkip).Take(pageSize).ToList();
            this.ViewBag.Pager = paper;
            this.ViewBag.searchText = searchText;
            return View(data);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.EM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.IdU == id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["IdPosition"] = new SelectList(_context.Positions, "IdPosition", "Position1");
            ViewData["IdD"] = new SelectList(_context.Departments, "IdD", "Department1");
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.EM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            ViewData["IdPosition"] = new SelectList(_context.Positions, "IdPosition", "Position1");
            ViewData["IdD"] = new SelectList(_context.Departments, "IdD", "Department1");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.EM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (user.ImageUpload != null && user.ImageUpload.Length > 0)
                    {
                        // lưu ảnh mới
                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/Avatar");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + user.ImageUpload.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var filesStream = new FileStream(filePath, FileMode.Create))
                        {
                            await user.ImageUpload.CopyToAsync(filesStream);
                        }
                        user.Avatar = "/images/Avatar/" + uniqueFileName;
                    }

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error: " + ex.Message);
                }
            }

            ViewData["IdPosition"] = new SelectList(_context.Positions, "IdPosition", "Position1");
            ViewData["IdD"] = new SelectList(_context.Departments, "IdD", "Department1");

            return View(user);
        }


        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.EM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            ViewData["IdPosition"] = new SelectList(_context.Positions, "IdPosition", "Position1");
            ViewData["IdD"] = new SelectList(_context.Departments, "IdD", "Department1");
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,User user)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.EM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id != user.IdU)
            {
                return NotFound();
            }
            

            if (ModelState.IsValid)
            {
                try
                {
                    var oldUser = await _context.Users.FindAsync(id);

                    if (oldUser == null)
                    {
                        return NotFound();
                    }
                    if (user.ImageUpload != null && user.ImageUpload.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(user.Avatar))
                        {
                            string oldAvatarPath = Path.Combine(_hostingEnvironment.WebRootPath, user.Avatar.TrimStart('/'));
                            if (System.IO.File.Exists(oldAvatarPath))
                            {
                                System.IO.File.Delete(oldAvatarPath);
                            }
                        }
                        // lưu ảnh mới
                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/Avatar");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + user.ImageUpload.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var filesStream = new FileStream(filePath, FileMode.Create))
                        {
                            await user.ImageUpload.CopyToAsync(filesStream);
                        }
                        user.Avatar = "/images/Avatar/" + uniqueFileName;
                    }
                    else
                    {
                        user.Avatar = user.Avatar; // giữ nguyên ảnh cũ nếu không có ảnh mới
                    }
                    _context.Entry(oldUser).CurrentValues.SetValues(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.IdU))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction("HomeHome","Home");

            }

            ViewData["IdPosition"] = new SelectList(_context.Positions, "IdPosition", "Position1");
            ViewData["IdD"] = new SelectList(_context.Departments, "IdD", "Department1");

            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.EM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.IdU == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.EM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (_context.Users == null)
            {
                return Problem("Entity set 'ManagerContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.IdU == id)).GetValueOrDefault();
        }
    }
}
