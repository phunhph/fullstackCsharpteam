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
using fullstackCsharp.Models.ViewModel.Salaries;
using fullstackCsharp.DAO;
using fullstackCsharp.Models.ViewModel.Attendances;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace fullstackCsharp.Controllers
{
    [TypeFilter(typeof(AuthorizeFilter))]
    public class SalariesController : Controller
    {
        private readonly ManagerContext _context;
        private readonly IPayrollService _payrollService;
        private readonly SalaryDAO _salaryDAO;
        public SalariesController(ManagerContext context, IPayrollService payrollService, SalaryDAO salaryDAO)
        {
            _context = context;
            _payrollService = payrollService;
            _salaryDAO = salaryDAO;
        }
        public IActionResult ExportToExcel(string searchString, SearchModel search = null)
        {
            // Lấy dữ liệu tương tự như trong action InformationSalary
            List<TotalSalaryViewModel> totalSalaryViews = _salaryDAO.totalSalaryViewModels(search).ToList();

            // Lọc dữ liệu nếu có từ khóa tìm kiếm
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                var searchTiengViet = searchString.Normalize(NormalizationForm.FormD);
                totalSalaryViews = totalSalaryViews
                    .Where(u => u.FullName.Normalize(NormalizationForm.FormD).ToLower().Contains(searchTiengViet.ToLower())).ToList();
            }

            // Tạo file Excel mới
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Salary Information");

                // Thêm tiêu đề cho các cột
                worksheet.Cells[2, 6].Value = "Tháng";
                worksheet.Cells[2, 8].Value = "Năm";
                worksheet.Cells[3, 7].Value = "công đủ tháng";

                worksheet.Cells[4, 2].Value = "ID";
                worksheet.Cells[4, 3].Value = "Nhân viên";
                worksheet.Cells[4, 4].Value = "Công thực tế";
                worksheet.Cells[4, 5].Value = "Lương cơ bản";
                worksheet.Cells[4, 6].Value = "Hệ số cấp bậc";
                worksheet.Cells[4, 7].Value = "Phụ cấp";
                worksheet.Cells[4, 8].Value = "Lương ";
                worksheet.Cells[4, 9].Value = "Thưởng phạt";
                worksheet.Cells[4, 10].Value = "Lương thực nhận";
                // Tiếp tục thêm tiêu đề cho các cột còn lại

                // Đổ dữ liệu vào các ô tương ứng
                for (int i = 0; i < totalSalaryViews.Count; i++)
                {
                    worksheet.Cells[i + 2, 7].Value = totalSalaryViews[i].Month;
                    worksheet.Cells[i + 2, 9].Value = totalSalaryViews[i].Year;
                    worksheet.Cells[i + 3, 8].Value = totalSalaryViews[i].FullWorkMonth;
                    worksheet.Cells[i + 5, 2].Value = totalSalaryViews[i].IdU;
                    worksheet.Cells[i + 5, 3].Value = totalSalaryViews[i].FullName;
                    worksheet.Cells[i + 5, 4].Value = totalSalaryViews[i].RealityWork;
                    worksheet.Cells[i + 5, 5].Value = totalSalaryViews[i].BasicSalary;
                    worksheet.Cells[i + 5, 6].Value = totalSalaryViews[i].Coefficient;
                    worksheet.Cells[i + 5, 7].Value = totalSalaryViews[i].AllowanceAmount;
                    worksheet.Cells[i + 5, 8].Value = totalSalaryViews[i].Salary;
                    worksheet.Cells[i + 5, 9].Value = totalSalaryViews[i].TotalPayOff;
                    worksheet.Cells[i + 5, 10].Value = totalSalaryViews[i].TotalSalaryMonth;
                    // Tiếp tục đổ dữ liệu cho các ô còn lại
                }
                int numberOfColumns = 13;
                // Tùy chỉnh định dạng cho bảng dữ liệu
                var headerCells = worksheet.Cells[1, 1, 1, numberOfColumns];
                headerCells.Style.Font.Bold = true;
                headerCells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerCells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                // Autofit cột
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Chuyển đổi dữ liệu Excel thành mảng byte
                byte[] excelBytes = package.GetAsByteArray();

                // Trả về file Excel
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalaryInformation.xlsx");
            }
        }

        public IActionResult InformationSalary(string searchString, SearchModel search = null, int pg = 1)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            //lọc năm
            if (search == null)
            {
                search = new SearchModel();
            }

            List<TotalSalaryViewModel> totalSalaryViews = _salaryDAO.totalSalaryViewModels(search).ToList();

            //tìm kiếm
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower(); // chuyển tất cả thành chữ thường
                var searchTiengViet = searchString.Normalize(NormalizationForm.FormD);
                totalSalaryViews = totalSalaryViews
                    .Where(u => u.FullName.Normalize(NormalizationForm.FormD).ToLower().Contains(searchTiengViet.ToLower())).ToList();
                if (totalSalaryViews.Count == 0)
                {
                    ViewBag.SearchResultMessage = "Không tìm thấy kết quả phù hợp";
                }
            }
            ViewBag.SearchModel = search;
            const int pageSize = 10;
            if (pg < 1) pg = 1;
            int recsCount = totalSalaryViews.Count();
            var paper = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = totalSalaryViews.Skip(recSkip).Take(pageSize).ToList();
            this.ViewBag.Pager = paper;
            ViewData["CurrentFilter"] = searchString;
            return View(data);
        }

        public IActionResult SalarySelf( int idu,SearchModel search = null)
        {
            if (search == null)
            {
                search = new SearchModel();
            }
             idu = HttpContext.Session.GetInt32("idu").Value;
            var fullname = HttpContext.Session.GetString("fullname");
            List<TotalSalaryViewModel> salarySelf = _salaryDAO.totalSalarySelf(search, idu).ToList();
            ViewBag.fullname  = fullname;
            ViewBag.Month = search.Month;
            ViewBag.Year = search.Year;

            return View(salarySelf);
        }


        // GET: Salaries
        public async Task<IActionResult> Index(string searchText = "", int pg = 1)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            var managerContext = _context.Salaries.Include(s => s.IdUNavigation).ToList();
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
            this.ViewBag.searchText = searchText;
            return View(data);
        }

        // GET: Salaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
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
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "FullName");
            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Salary salary)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(salary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "FullName", salary.IdU);
            return View(salary);
        }

        // GET: Salaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
            if (id == null || _context.Salaries == null)
            {
                return NotFound();
            }

            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null)
            {
                return NotFound();
            }
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "FullName", salary.IdU);
            return View(salary);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Salary salary)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
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
            ViewData["IdU"] = new SelectList(_context.Users, "IdU", "FullName", salary.IdU);
            return View(salary);
        }

        // GET: Salaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
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
            if (!AuthorizationManager.CkeckUserRole(HttpContext, (int)RoleNameId.SM, (int)RoleNameId.admin
                ))
            {
                return RedirectToAction("NullRole", "Home");
            }
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
