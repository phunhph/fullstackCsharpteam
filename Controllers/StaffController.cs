using fullstackCsharp.DAO;
using fullstackCsharp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using NuGet.Packaging.Signing;
using System.Collections.Generic;
using System.Net;
using System.Security;

namespace fullstackCsharp.Controllers
{
    public class StaffController : Controller
    {
        //=============================================== Seleted function ======================================================//
        // GET: StaffController
        public ActionResult Index()
        {
            StaffDAO staffDAO = new StaffDAO();
            List<Staff> staffList = staffDAO.SelectAll();//new List<Staff>();
            ViewData["staffList"] = staffList;
            return View();
        }
        //public ActionResult Add()
        //{
        //    StaffDAO staffDAO = new StaffDAO();
        //    List<Staff> staffList = staffDAO.SelectAll();
        //    ViewData["staffList"] = staffList;
        //    return View();
        //}

        //=====================================================================================================//
        // GET: StaffController/Details/5
        //public ActionResult Details(int id)
        //{
        //    // gia su admin moi vao duoc trang nay
        //    // DDee anh mo lai sau =))
        //    StaffDAO staffDAO = new StaffDAO();

        //    var tryGetCookie = HttpContext.Request.Cookies["myCookies"];
        //    if (tryGetCookie != null)
        //    {
        //        // return admin view
        //        Console.WriteLine("Loged in user is admin!");
        //        return View();
        //    }
        //    return RedirectToAction("~/Login");
        //}

        //===========================================fuction create====================================================//

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
            StaffDAO staffDAO = new StaffDAO();


            Microsoft.Extensions.Primitives.StringValues manv;
            collection.TryGetValue("manv", out manv);
            Microsoft.Extensions.Primitives.StringValues namenv;
            collection.TryGetValue("namenv", out namenv);
            Microsoft.Extensions.Primitives.StringValues gender;
            collection.TryGetValue("gender", out gender);
            Microsoft.Extensions.Primitives.StringValues phone;
            collection.TryGetValue("phone", out phone);
            Microsoft.Extensions.Primitives.StringValues address;
            collection.TryGetValue("address", out address);
            Microsoft.Extensions.Primitives.StringValues user;
            collection.TryGetValue("user", out user);
            Microsoft.Extensions.Primitives.StringValues password;
            collection.TryGetValue("password", out password);
            Microsoft.Extensions.Primitives.StringValues status;
            collection.TryGetValue("status", out status);
            Microsoft.Extensions.Primitives.StringValues rankString;
            collection.TryGetValue("rankString", out rankString);
            Microsoft.Extensions.Primitives.StringValues id_pb;
            collection.TryGetValue("id_pb", out id_pb);
            Microsoft.Extensions.Primitives.StringValues id_rank;
            collection.TryGetValue("id_rank", out id_rank);
            int rank;

            bool successfullyParsed = int.TryParse(rankString, out rank);
            if (successfullyParsed)
            {
                // sử dụng giá trị
                Console.WriteLine(rank);
            }
            else
            {
                // báo lỗi ra giao diện
                // Console.WriteLine("lỗi");
                ViewData["error"] = "Delete Lỗi rồi";
            }
            // Staff newStaff = new Staff("l3", "hieu", "1234567", "nữ", "hjk", "gvguv", "5655", 2,"active", "PB2", "R2");
            Staff newStaff = new Staff(manv, namenv, phone, gender, address, user, password, rank, status, id_pb, id_rank);
            List<Staff> staff = staffDAO.Create(newStaff);


            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //=============================================== deleted function ===================================================//

        // GET: StaffController/Delete/5
        public ActionResult Delete()
        {
            //StaffDAO staffDAO = new StaffDAO();
            //List<Staff> staffList = staffDAO.SelectAll();//new List<Staff>();
            //ViewData["staffList"] = staffList;
            return View();
        }

        // POST: StaffController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(String deletelist)
        {
            List<string> ids = new List<string>(deletelist.Split(","));

            StaffDAO staffDAO = new StaffDAO();

            try
            {
                foreach (var id in ids)
                {
                    staffDAO.Delete(id);
                }
            }
            catch
            {
                ViewData["error"] = "Delete Lỗi rồi";
                return View();
            }
            //List<Staff> staff = staffDAO.SelectAll();
            return RedirectToAction(nameof(Index));
        }
        //============================================= function edit ===================================================//

        // GET: StaffController/Edit/5
        [Route("Staff/Edit/{id?}")]
        public ActionResult Edit( String id)
        {
            StaffDAO staffDAO = new StaffDAO();
           Staff staff = staffDAO.SelectByID(id) ;
            ViewData["staff"] = staff;
            return View();
        }

        // POST: StaffController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Route("Staff/Edit/{id?}")]
        public ActionResult Edit( String edit1, IFormCollection collection)
        {
            StaffDAO staffDAO = new StaffDAO();

            Microsoft.Extensions.Primitives.StringValues namenv;
            collection.TryGetValue("Namenv", out namenv);
            Microsoft.Extensions.Primitives.StringValues gender;
            collection.TryGetValue("gender", out gender);
            Microsoft.Extensions.Primitives.StringValues phone;
            collection.TryGetValue("phone", out phone);
            Microsoft.Extensions.Primitives.StringValues address;
            collection.TryGetValue("Address", out address);
            Microsoft.Extensions.Primitives.StringValues user;
            collection.TryGetValue("User", out user);
            Microsoft.Extensions.Primitives.StringValues password;
            collection.TryGetValue("Password", out password);
            Microsoft.Extensions.Primitives.StringValues status;
            collection.TryGetValue("Status", out status);
            Microsoft.Extensions.Primitives.StringValues rankString;
            collection.TryGetValue("rankString", out rankString);
            Microsoft.Extensions.Primitives.StringValues id_pb;
            collection.TryGetValue("id_pb", out id_pb);
            Microsoft.Extensions.Primitives.StringValues id_rank;
            collection.TryGetValue("id_rank", out id_rank);
            int rank;

            bool successfullyParsed = int.TryParse(rankString, out rank);
            if (successfullyParsed)
            {
                // sử dụng giá trị
                Console.WriteLine(rank);
            }
            else
            {
                // báo lỗi ra giao diện
                // Console.WriteLine("lỗi");
                ViewData["error"] = "Delete Lỗi rồi";
            }
            // Staff newStaff = new Staff("l3", "hieu", "1234567", "nữ", "hjk", "gvguv", "5655", 2,"active", "PB2", "R2");
            Staff staff = new Staff(edit1, namenv, phone, gender, address, user, password, rank, status, id_pb, id_rank);

            staffDAO.Edit(staff,edit1);

            try
            {
               // staffDAO.Edit(newStaff ,edit1);

            }
            catch
            {
                ViewData["error"] = "Edit Lỗi rồi";
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}

