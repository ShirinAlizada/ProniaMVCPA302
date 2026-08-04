using Microsoft.AspNetCore.Mvc;

namespace ProniaMVCPA302.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
