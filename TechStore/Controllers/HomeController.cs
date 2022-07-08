using Microsoft.AspNetCore.Mvc;

namespace TechStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
