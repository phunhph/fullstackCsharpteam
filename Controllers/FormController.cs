﻿using fullstackCsharp.DAO;
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
            var rank = HttpContext.Request.Cookies["rank"];
            if (rank == "2")
            {
                // return admin view
                Console.WriteLine("Loged in user is admin!");
                return RedirectToAction("Admin");
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
            FormDAO formDAO = new FormDAO();
            List<Form> formList = formDAO.Select();
            ViewData["formList"] = formList;
            return View();
        }
        // push form
        [HttpPost]
        public ActionResult FormPush(Form form)
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
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteForm(Form form)
        {
            Console.WriteLine("check"+form.Soform);
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
