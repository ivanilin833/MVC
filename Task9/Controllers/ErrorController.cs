using Microsoft.AspNetCore.Mvc;

namespace Task9.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
