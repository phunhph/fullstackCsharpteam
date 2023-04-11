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
            if (rank == "1")
            {
                // return admin view
                Console.WriteLine("Loged in user is admin!");
                return View();
            }
            else if (rank == "2")
            {
                // return user view
                Console.WriteLine("Loged in user is nomal user!");
                return View();
            }
            return View();
        }
        // checkin
        [HttpPost]
		public ActionResult Checkin(Diemdanh diemdanh)
		{
				bool CheckIn = DiemDanhDAO.CommitIn(diemdanh) ;
				if (CheckIn)
				{
                Response.Cookies.Append("id", diemdanh.id);
                Response.Cookies.Append("id_nv", diemdanh.id_nv);
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
