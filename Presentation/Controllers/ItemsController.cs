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
            string viewType = HttpContext.Request.Query["viewType"];
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
                if (allResturants.Any(x => x.GetValidators().Contains(loggedInName)))
                {
                    // user is an admin
                    items = allResturants.ToList<IItemValidating>();
                }
                else
                {
                    items = allResturants.Where(x => x.Status == "approved").ToList<IItemValidating>();
                }

            }
            else if (viewType == "list")
            {
                var allMenuItems = itemsRepository.GetMenuItems().ToList();

                items = allMenuItems.Where(x => x.Resturant.Status == "approved").ToList<IItemValidating>();
            }

            return View(items);
        }

        public IActionResult Details(int id, [FromKeyedServices("db")] IItemsRepository itemsRepository)
        {
            var resturant = itemsRepository.GetResturants().FirstOrDefault(x => x.Id == id);
            var menuItems = itemsRepository.GetMenuItems().Where(x => x.Resturant.Id == id && x.Status == "approved").ToList();

            ViewBag.Resturant = resturant;

            return View(menuItems);
        }

        [AuthorizeFilter]
        [HttpPost("Items/Approve/{viewType}")]
        public IActionResult Approve(string viewType, string[] ids, [FromKeyedServices("db")] IItemsRepository itemsRepository)
        {
            foreach (var id in ids)
            {
                if (int.TryParse(id, out int resturantId))
                {
                    // this is a resturant, since resturant ids are ints
                    itemsRepository.ApproveResturant(resturantId);
                }
                else if (Guid.TryParse(id, out Guid menuItemId))
                {
                    // this is a menu item, since menu item ids are GUIDs
                    itemsRepository.ApproveMenuItem(menuItemId);
                }
            }

            if (viewType == "list")
            {
                return Redirect("/Items/Catalog?viewType=list");
            }
            else
            {
                return Redirect("/Items/Catalog?viewType=card");
            }
        }
    }
}
