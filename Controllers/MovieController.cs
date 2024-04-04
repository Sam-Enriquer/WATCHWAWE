using Microsoft.AspNetCore.Mvc;

namespace WATCHWAWE.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Naruto()
        {
            return View();
        }
    }
}
