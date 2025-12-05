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
            string loggedInName = User.Identity.Name;

            // set default
            if (viewType != "card" && viewType != "list")
            {
                viewType = "card";
            }

            ViewBag.ViewType = viewType;

            List<IItemValidating> items = new List<IItemValidating>();

            if (viewType == "card")
            {
                var allResturants = itemsRepository.GetResturants().ToList();

                items = allResturants.Where(x => x.Status == "approved").ToList<IItemValidating>();

            }
            else if (viewType == "list")
            {
                var allMenuItems = itemsRepository.GetMenuItems().ToList();

                items = allMenuItems.Where(x => x.Status == "approved").ToList<IItemValidating>();
            }

            return View(items);
        }
    }
}
