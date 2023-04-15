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
                Console.WriteLine("Loged in user is admin!");
                return RedirectToAction("Admin");
               // return View();
            }
            else if (rank == "1")
            {
                // return user view
                Console.WriteLine("Loged in user is nomal user!");
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
            var id_nv = HttpContext.Request.Cookies["id_nv"];
            diemdanh.id_nv = id_nv;
            diemdanh.name = HttpContext.Request.Cookies["name"];
            Response.Cookies.Append("id", diemdanh.id);
            bool CheckIn = DiemDanhDAO.CommitIn(diemdanh) ;
				if (CheckIn)
				{     
                
                Console.WriteLine("Xác nhận time in");       
                }
				else
				{
                Console.WriteLine("error checkin");
                }
            return RedirectToAction("Index");
        }
        //checkout
        [HttpPost]
        public ActionResult Checkout(Diemdanh diemdanh)
        {
            var id_nv = HttpContext.Request.Cookies["id_nv"];
            diemdanh.id_nv = id_nv;
            diemdanh.name = HttpContext.Request.Cookies["name"];
            diemdanh.id= HttpContext.Request.Cookies["id"];
            bool CheckOut = DiemDanhDAO.CommitOut(diemdanh);
            if (CheckOut)
            {
                Console.WriteLine("Xác nhận time out");
            }
            else
            {
                Console.WriteLine("error time out");
            }
            return RedirectToAction("Index");

        }
    }
}
