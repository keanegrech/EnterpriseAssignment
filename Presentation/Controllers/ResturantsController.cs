using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ResturantsController : Controller
    {
        public IActionResult Index()
        {
            string viewType = HttpContext.Request.Query["viewtype"];
            Console.WriteLine(viewType);

            return View();
        }
    }
}
