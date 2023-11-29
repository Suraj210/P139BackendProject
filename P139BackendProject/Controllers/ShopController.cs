using Microsoft.AspNetCore.Mvc;

namespace P139BackendProject.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductDetails()
        {
            return View();
        }
    }
}
