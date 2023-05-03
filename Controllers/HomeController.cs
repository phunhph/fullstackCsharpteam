using fullstackCsharp.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RestSharp;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Xml;

namespace fullstackCsharp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index(LoginDAO loginDAO)
        {
            var rank = HttpContext.Request.Cookies["rank"];
            if (rank == null)
            {
                return RedirectToAction("Login");
            }
             else if (rank == "5")
            {
                // return staff view
                Console.Write("staff");
                return View();
            }
            else if (rank == "1")
            {
                // return admin view
                return View();
            }
            return RedirectToAction("Login");
        }
        //đăng xuất
        public IActionResult Logout()
        {
            Response.Cookies.Delete("username");
            Response.Cookies.Delete("name");
            Response.Cookies.Delete("rank");
            Response.Cookies.Delete("id_nv");
            return RedirectToAction("Login");
        }
        //đăng nhập
        //check csdl
        private LoginDAO loginDAO = new LoginDAO();
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(Login Login)
        {
            if (ModelState.IsValid)
            {
                bool isValidUser = loginDAO.ValidateUser(Login);
                if (isValidUser)
                {
                    Response.Cookies.Append("username", Login.user);
                    Response.Cookies.Append("name", Login.name);
                    Response.Cookies.Append("id_nv", Login.id_nv);
                    Response.Cookies.Append("rank", Login.rank.ToString());
                    Response.Cookies.Delete("error");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "đăng nhập thất bại";
                }
            }
            return View(Login);
        }
        // quên mật khẩu
        [HttpPost]
        public ActionResult SendEmail(Email email)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com",587);
                mail.To.Add(email.To);
                mail.Subject = email.Subject;
                mail.Body = email.Body;
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential("nguyenhuuphu190502004@gmail.com", "001204036687a");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                ViewBag.Message = "Email sent successfully";
                Console.WriteLine("Email sent successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: + ex.Message.ToString()");
                ViewBag.Message = "Error:" + ex.Message.ToString();
            }

            return RedirectToAction("Login");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}