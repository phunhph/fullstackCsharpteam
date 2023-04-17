using fullstackCsharp.DAO;
using fullstackCsharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace fullstackCsharp.Controllers
{
    public class DiemdanhController : Controller
    {
        private DiemDanhDAO DiemDanhDAO = new DiemDanhDAO();

        public ActionResult Index()
        {
            
            var id_nv = HttpContext.Request.Cookies["id_nv"];
            DiemDanhDAO diemdanhDAO = new DiemDanhDAO();
            List<Diemdanh> diemdanhList = diemdanhDAO.Select(id_nv);
            ViewData["diemdanhList"] = diemdanhList;    
            var rank = HttpContext.Request.Cookies["rank"];
            if (rank == "2")
            {
                // return admin view
                return RedirectToAction("Admin");
               // return View();
            }
            else if (rank == "1")
            {
                // return user view
                return View();
            }
            return View();
        }
        public ActionResult Admin()
        {
            DiemDanhDAO diemdanhDAO = new DiemDanhDAO();
            List<Diemdanh> diemdanhList = diemdanhDAO.Select();
            ViewData["diemdanhList"] = diemdanhList;
            return View();
        }
        // checkin
        [HttpPost]
		public ActionResult Checkin(Diemdanh diemdanh)
		{
            // suất dữ liệu
            var id_nv = HttpContext.Request.Cookies["id_nv"];
            DiemDanhDAO diemdanhDAO = new DiemDanhDAO();
            List<Diemdanh> diemdanhList = diemdanhDAO.Select(id_nv);
            ViewData["diemdanhList"] = diemdanhList;
            // lấy dữ liệu để insert
            diemdanh.id_nv = id_nv;
            diemdanh.name = HttpContext.Request.Cookies["name"];
            Response.Cookies.Append("id", diemdanh.id);
            bool CheckIn = DiemDanhDAO.CommitIn(diemdanh) ;
				if (CheckIn)
				{
                    ViewData["confirm"] = "Check in thành công";        
                }
				else
				{
                    ViewData["error"] = "Chỉ được check in 1 lần / ngày";
                
                }
            return View("Index");
        }
        //checkout
        [HttpPost]
        public ActionResult Checkout(Diemdanh diemdanh)
        {
            var id_nv = HttpContext.Request.Cookies["id_nv"];
            DiemDanhDAO diemdanhDAO = new DiemDanhDAO();
            List<Diemdanh> diemdanhList = diemdanhDAO.Select(id_nv);
            ViewData["diemdanhList"] = diemdanhList;
            // lấy dữ liệu để update
            diemdanh.id_nv = id_nv;
            diemdanh.name = HttpContext.Request.Cookies["name"];
            diemdanh.id= HttpContext.Request.Cookies["id"];
            bool CheckOut = DiemDanhDAO.CommitOut(diemdanh);
            if (CheckOut)
            {
                ViewData["confirm"] = "Check out thành công"; 
            }
            else
            {
                ViewData["error"] = "Check out thất bại";
            }
            return View("Index");

        }
        // fixcheckout
        [HttpPost]
        public ActionResult FixCheck(Diemdanh diemdanh)
        {
            bool FixCheckOut = DiemDanhDAO.FixCheck(diemdanh);
            if (FixCheckOut)
            {
                ViewData["confirm"] = "Check out thành công";
            }
            else
            {
                ViewData["error"] = "Check out thất bại";
            }
            return RedirectToAction("Index");

        }
    }
}
