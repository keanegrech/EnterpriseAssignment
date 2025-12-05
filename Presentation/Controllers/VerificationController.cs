using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class VerificationController : Controller
    {
        [Authorize]
        public IActionResult Verification(int? id, [FromKeyedServices("db")] IItemsRepository itemsRepository)
        {
            string loggedInName = User.Identity.Name;

            Resturant resturant = new Resturant();

            if (resturant.GetValidators().Contains(loggedInName))
            {
                // this is an admin
                var pendingResturants = itemsRepository.GetResturants().ToList().Where(x => x.Status == "pending");

                return View("Resturants", pendingResturants);
            }
            else
            {
                var menuItems = itemsRepository.GetMenuItems().ToList().Where(x => x.Resturant.OwnerEmailAddress == loggedInName).DistinctBy(x=> x.Resturant.Id);

                if (id.HasValue)
                {
                    menuItems = itemsRepository.GetMenuItems().ToList().Where(x => x.Resturant.Id == id.Value && x.Status == "pending" && x.Resturant.Status == "approved");
                    return View("MenuItems", menuItems);
                }

                return View("OwnedResturants", menuItems);
            }
        }

        [AuthorizeFilter]
        [HttpPost]
        public IActionResult Approve(string[] ids, [FromKeyedServices("db")] IItemsRepository itemsRepository)
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

            return RedirectToAction("Verification");
        }
    }
}
