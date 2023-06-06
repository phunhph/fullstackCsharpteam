﻿using fullstackCsharp.DAO;
using fullstackCsharp.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RestSharp;
using System.Diagnostics;
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
             else if (rank == "0")
            {
                // return login view
                Console.WriteLine("tài khoản đã bị vô hiệu hoá");
                return RedirectToAction("Login");
            }
            else if (rank == "1")
            {
                // return staff view
                Console.WriteLine("tài khoản của nhân viên");
                return View();
            }
            else if (rank == "2")
            {
                // return admin view
                Console.WriteLine("tài khoản của admin");
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
            return RedirectToAction("Login");
        }
        //đăng nhập
        //check csdl why did it jump in here???
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
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}