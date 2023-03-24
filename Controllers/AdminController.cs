using fullstackCsharp.DAO;
using fullstackCsharp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace fullstackCsharp.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController
        public ActionResult Index()
        {
            // gia su admin moi vao duoc trang nay
            var privilate = HttpContext.Request.Cookies["privilate"];
            if (privilate == "admin")
            {
                // return admin view
                Console.WriteLine("Loged in user is an admin!");
            } else
            {
                Console.WriteLine("Loged in user is not an admin!");
                return RedirectToAction("Index", "Home");
            }


            AdminDAO adminDAO = new AdminDAO();
            adminDAO.Select("001");
            return View();
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            AdminDAO adminDAO = new AdminDAO();
            List<Admin> Admins = adminDAO.Select("001");
            ViewData["Admins"] = Admins;
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
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

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
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

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
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
