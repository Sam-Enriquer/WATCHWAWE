using Microsoft.AspNetCore.Mvc;

namespace WATCHWAWE.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
