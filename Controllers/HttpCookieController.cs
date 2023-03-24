using Microsoft.AspNetCore.Mvc;

namespace fullstackCsharp.Controllers
{
    public class HttpCookieController : Controller
    {
        public ActionResult cookies()
        {
            Response.Cookies.Append("website", "WebTrainingRoom");

            return View();
        }
    }
}
