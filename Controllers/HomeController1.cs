using Microsoft.AspNetCore.Mvc;

namespace fullstackCsharp.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
