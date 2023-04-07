using fullstackCsharp.DAO;
using fullstackCsharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace fullstackCsharp.Controllers
{
    public class DiemdanhController : Controller
    {
        // khai báo đối tượng theo Diemdanh và DiemdanhDAO để lưu dữ liệu
        private DiemDanhDAO DiemDanhDAO = new DiemDanhDAO();
        private Diemdanh rc = new Diemdanh();
        public IActionResult Index(Diemdanh diemdanh)
        {
            // lấy thông tin để insert vào DB 
            var id_nv = HttpContext.Request.Cookies["id_nv"];
            var id = "4";
            var time_in = "2023/01/03";
            var time_out = "2023/01/03";
            bool Commitout = DiemDanhDAO.Commitout(diemdanh, id_nv,time_out,id);
            bool Commitin = DiemDanhDAO.Commitin(diemdanh, id_nv,id,time_in);
            if (Commitout)
            {
                // check xem đã điểm danh vào chưa
                rc.id_nv = diemdanh.id_nv;
                rc.name = diemdanh.name;
                Console.WriteLine("Mã Nhân Viên: " + rc.id_nv);
                Console.WriteLine("Tên Nhân Viên: " + rc.name);
                Console.WriteLine("Xác nhận time out");
                return View();
            }
            else if(Commitin)
            {// nếu chưa thì đưa dữ liệu vào
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
