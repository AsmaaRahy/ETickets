using Microsoft.AspNetCore.Mvc;

namespace ETickets.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
