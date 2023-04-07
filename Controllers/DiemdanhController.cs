using fullstackCsharp.DAO;
using fullstackCsharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace fullstackCsharp.Controllers
{
    public class DiemdanhController : Controller
    {
       

        private DiemDanhDAO DiemDanhDAO = new DiemDanhDAO();
        private Diemdanh rc = new Diemdanh();
        public IActionResult Index(Diemdanh diemdanh)
        {
            var id_nv = HttpContext.Request.Cookies["id_nv"];
            var id = HttpContext.Request.Cookies["id_nv"];
            var time_in = "2023/01/03";
            bool Commitout = DiemDanhDAO.Commitout(diemdanh, id_nv);
            bool Commitin = DiemDanhDAO.Commitin(diemdanh, id_nv,id,time_in);
            if (Commitout)
            {
                rc.id_nv = diemdanh.id_nv;
                rc.name = diemdanh.name;
                Console.WriteLine("Mã Nhân Viên: " + rc.id_nv);
                Console.WriteLine("Tên Nhân Viên: " + rc.name);
                Console.WriteLine("Xác nhận time out");
                return View();
            }
            else if(Commitin)
            {
                rc.id_nv = diemdanh.id_nv;
                rc.name = diemdanh.name;
                Console.WriteLine("Mã Nhân Viên: " + rc.id_nv);
                Console.WriteLine("Tên Nhân Viên: " + rc.name);
                Console.WriteLine("Xác nhận time in");
                return View();

            }
            return View();
        }
    }
}
