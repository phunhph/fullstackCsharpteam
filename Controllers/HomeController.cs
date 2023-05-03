using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fullstackCsharp.Models;
using fullstackCsharp.Models.TableAccessories;

using System.Collections.Generic;
using System.Diagnostics;
using static fullstackCsharp.Models.TableAccessories.TotalSalaryViewModel;

namespace fullstackCsharp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ManagerContext _context;


        public HomeController(ILogger<HomeController> logger, ManagerContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult HomeHome()
        {
            return View();
        }

        public IActionResult NullRole()
        {
            return View();
        }
/*
       public IActionResult GetInformationSalary(int month, int year)
        {
            var userPosition = _context.Users.Include(u => u.IdPositionNavigation).ToList();
            var payoffdate = _context.Payoffs
                            .Where(p => p.PayoffDate.Value.Month == month && p.PayoffDate.Value.Year == year);
            
            var salariesAll = userPosition.Select(e => new
            {
                StaffName = e.FullName,
                PositionName = e.IdPositionNavigation.Position1,
                PositionCoefficient = e.IdPositionNavigation.Coefficient,
                BasicSalary = e.Salaries

            });
                               
            return View();
        }*/




            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}