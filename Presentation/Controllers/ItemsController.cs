using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Index()
        {
            string viewType = HttpContext.Request.Query["viewtype"];
            Console.WriteLine(viewType);

            return View();
        }
    }
}
