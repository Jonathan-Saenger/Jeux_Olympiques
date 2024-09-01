using Microsoft.AspNetCore.Mvc;

namespace Jeux_Olympiques.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
