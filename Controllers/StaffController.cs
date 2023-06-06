using fullstackCsharp.DAO;
using fullstackCsharp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Collections.Generic;

namespace fullstackCsharp.Controllers
{
    public class StaffController : Controller
    {

        //================================================================================ selected====================================================================//
        // GET: StaffController
        public ActionResult Index()
        {
            StaffDAO staffDAO = new StaffDAO();
            List<Staff> staffList =   staffDAO.SelectAll();//new List<Staff>();
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

        // GET: StaffController/Details/5
        public ActionResult Details(int id)
        {
            // gia su admin moi vao duoc trang nay
            // DDee anh mo lai sau =))
            StaffDAO staffDAO = new StaffDAO();
           
            var tryGetCookie = HttpContext.Request.Cookies["myCookies"];
            if (tryGetCookie != null)
            {
                // return admin view
                Console.WriteLine("Loged in user is admin!");
                return View();
            }
            return RedirectToAction("~/Login");
        }

        //================================================================================ create====================================================================//
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
            Microsoft.Extensions.Primitives.StringValues id_nv;
            collection.TryGetValue("id", out id_nv);
            Microsoft.Extensions.Primitives.StringValues namenv;
            collection.TryGetValue("name", out namenv);
            Microsoft.Extensions.Primitives.StringValues gender;
            collection.TryGetValue("gender", out gender);
            Microsoft.Extensions.Primitives.StringValues phone;
            collection.TryGetValue("phone", out  phone);
            Microsoft.Extensions.Primitives.StringValues address;
            collection.TryGetValue("address", out address);
            Microsoft.Extensions.Primitives.StringValues user;
            collection.TryGetValue("user", out user);
            Microsoft.Extensions.Primitives.StringValues password;
            collection.TryGetValue("password", out password);
            Microsoft.Extensions.Primitives.StringValues rankString;
            collection.TryGetValue("rankString", out rankString);
            Microsoft.Extensions.Primitives.StringValues status;
            collection.TryGetValue("status", out status);
            Microsoft.Extensions.Primitives.StringValues id_pb;
            collection.TryGetValue("id_pb", out id_pb);
            Microsoft.Extensions.Primitives.StringValues id_rank;
            collection.TryGetValue("id_rank", out id_rank);

            int rank;
            

            bool successfullyParsed = int.TryParse(rankString, out rank);
            if (successfullyParsed)
            {
                // sử dụng giá trị
                Console.WriteLine("succefull");
            }
            else
            {
                // báo lỗi ra giao diện
                Console.WriteLine("fail");
            }
            Staff newstaff = new Staff(id_nv, namenv, gender,address, phone,user,password, rank, status,id_pb,id_rank);
            List<Staff> staffs = staffDAO.Create(newstaff);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
      
        //================================================================================ deleted====================================================================//

        // GET: StaffController/Delete/5
        public ActionResult Delete(String id)
        {
            return View();
        }

        // POST: StaffController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(String deleteList, IFormCollection collection)

        {

            List<string> ids = new List<string>(deleteList.Split(","));
            StaffDAO staffDAO = new StaffDAO();
            foreach (var id1 in ids)
            {
                staffDAO.Delete(id1);
            }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //================================================================================ edit====================================================================//
        // GET: StaffController/Edit/5
        [Route("Staff/Edit/{id?}")]
        public ActionResult Edit(String id)
        {
            StaffDAO staffDAO = new StaffDAO();
            Staff staff = staffDAO.SelectById(id.Trim());
            ViewData["staff"] = staff;
            return View();
        }

        // POST: StaffController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Staff/Edit/{id?}")]
        public ActionResult Edit(String id, IFormCollection collection)
        {
            StaffDAO staffDAO = new StaffDAO();

            Microsoft.Extensions.Primitives.StringValues namenv;
            collection.TryGetValue("name", out namenv);
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
            
            Staff staff = new Staff(id, namenv,  gender, phone, address, user, password, rank, status, id_pb, id_rank);

           

            try
            {
                staffDAO.Edit(id, staff);

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
