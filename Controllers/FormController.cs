using fullstackCsharp.DAO;
using fullstackCsharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace fullstackCsharp.Controllers
{
    public class FormController : Controller
    {
        private FormDAO FormDAO = new FormDAO();
        public ActionResult Index()
        {
            var id_nv = HttpContext.Request.Cookies["id_nv"];
            FormDAO formDAO = new FormDAO();
            List<Form> formList = formDAO.Select(id_nv);
            ViewData["formList"] = formList;
            List<Form> formListCf = formDAO.Selectcf(id_nv);
            ViewData["formListCf"] = formListCf;
            var rank = HttpContext.Request.Cookies["rank"];
            if (rank == "1")
            {
                // return admin view
                return RedirectToAction("Admin");
            }
            else if (rank == "5")
            {
                // return user view
                return View();
            }
            return View();
        }
        public ActionResult Admin()
        {
            FormDAO formDAO = new FormDAO();
            List<Form> formList = formDAO.Select();
            ViewData["formList"] = formList;
            return View();
        }
        // push form
        [HttpPost]
        public ActionResult FormPush(Form form)
        {
            var id_nv = HttpContext.Request.Cookies["id_nv"];           
            bool check = FormDAO.checkStaff(id_nv, form) ;
            if(check)
            {
                form.TrangThai = "Đã gửi";
                bool pushForm = FormDAO.CommitForm(form);
                if (pushForm)
                {
                    Console.WriteLine("Xác nhận form");
                }
                else
                {
                    Console.WriteLine("error form");
                }
            }
            else
            {
                TempData["errorForm"] = "mỗi ngày chỉ được nghỉ 8 tiếng vui lòng kiểm tra lại";
            }
            
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteForm(Form form)
        {
            bool pushForm = FormDAO.DeleteForm(form);
            if (pushForm)
            {
                Console.WriteLine("Xác nhận xoá form");
            }
            else
            {
                Console.WriteLine("error form");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Confirm(Form form)
        {
            bool pushForm = FormDAO.Confirm(form);
            if (pushForm)
            {
                Console.WriteLine("Xác nhận xoá form");
            }
            else
            {
                Console.WriteLine("error form");
            }
            return RedirectToAction("Index");
        }
    }
}
