using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Catalog([FromKeyedServices("db")] IItemsRepository itemsRepository)
        {
            string viewType = HttpContext.Request.Query["viewtype"];

            // set default
            if (viewType != "card" && viewType != "list")
            {
                viewType = "card";
            }

            ViewBag.ViewType = viewType;

            List<IItemValidating> items = new List<IItemValidating>();

            if (viewType == "card")
            {
                items = itemsRepository.GetResturants().ToList<IItemValidating>();
            }
            else if (viewType == "list")
            {
                items = itemsRepository.GetMenuItems().ToList<IItemValidating>();
            }

            return View(items);
        }
    }
}
