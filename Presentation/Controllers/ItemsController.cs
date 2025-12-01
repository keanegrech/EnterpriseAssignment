using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ItemsController : Controller
    {
        private ResturantRepository _resturantRepository;
        private MenuItemRepository _menuItemRepository;

        public ItemsController(
            ResturantRepository resturantRepository,
            MenuItemRepository menuItemRepository
            )
        {
            _resturantRepository = resturantRepository;
            _menuItemRepository = menuItemRepository;
        }

        public IActionResult Index()
        {
            string viewType = HttpContext.Request.Query["viewtype"];
            Console.WriteLine(viewType);

            List<Resturant> resturants = _resturantRepository.Get().ToList();
            List<MenuItem> menuItems = _menuItemRepository.Get().ToList();

            return View(resturants);
        }
    }
}
