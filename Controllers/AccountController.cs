using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fullstackCsharp.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using fullstackCsharp.Service;

namespace fullstackCsharp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ManagerContext _context;

        public AccountController (ManagerContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var lg = await _context.Users.Include(nr => nr.UserRoles)
                .FirstOrDefaultAsync(u => u.UserName == username && u.PassWord == password);

            if(lg == null)
            {
                ViewBag.Error = "Tài khoản hoặc mật khẩu không đúng";
                return View();
            }
            if (lg.Status == (int)CheckStatus.close)
            {
                ViewBag.Error = "Tài khoản của bạn đã bị khóa";
                return View();
            }
            var roleIds = lg.UserRoles.Select(ur=>ur.IdR).ToList();
            HttpContext.Session.SetString("roleIds", string.Join(",",roleIds));
            HttpContext.Session.SetString("username", lg.UserName);
            HttpContext.Session.SetString("password", lg.PassWord);
            HttpContext.Session.SetString("fullname", lg.FullName);
            HttpContext.Session.SetInt32("idu", lg.IdU);
            if (!String.IsNullOrEmpty(lg.Avatar))
            {
                HttpContext.Session.SetString("avatar", lg.Avatar);
            }

            return RedirectToAction("HomeHome", "Home");
        }

        public async Task<IActionResult> logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("password");
            HttpContext.Session.Remove("rolsIds");
            HttpContext.Session.Remove("fullname");
            HttpContext.Session.Remove("avatar");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }


    }
}
