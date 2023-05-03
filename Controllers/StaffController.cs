using fullstackCsharp.DAO;
using fullstackCsharp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace fullstackCsharp.Controllers
{
    public class StaffController : Controller
    {
        // GET: StaffController
        public ActionResult Index()
        {
            StaffDAO staffDAO = new StaffDAO();
            staffDAO.Select("002");
            var privilate = HttpContext.Request.Cookies["privilate"];
            if (privilate != null)
            {
                // return admin view
                Console.WriteLine("Loged in user is admin!");
                return View();
            }
            else if (privilate == "user")
            {
                // return user view
                Console.WriteLine("Loged in user is nomal user!");
                return View();
            }
            return RedirectToAction("Login");
        }

        // GET: StaffController/Details/5
        public ActionResult Details(int id)
        {
            // gia su admin moi vao duoc trang nay
            // DDee anh mo lai sau =))
            StaffDAO staffDAO = new StaffDAO();
            staffDAO.Select("002");
            var tryGetCookie = HttpContext.Request.Cookies["myCookies"];
            if (tryGetCookie != null)
            {
                // return admin view
                Console.WriteLine("Loged in user is admin!");
                return View();
            }
            return RedirectToAction("~/Login");
        }

        // GET: StaffController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: StaffController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        // POST: StaffController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StaffController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StaffController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
