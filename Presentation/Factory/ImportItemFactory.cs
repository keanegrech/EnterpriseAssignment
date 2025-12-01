using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json;

namespace Presentation.Factory
{
    public class ImportItemFactory
    {
        private ResturantRepository _resturantRepository;
        private MenuItemRepository _menuItemRepository;

        public ImportItemFactory (
            ResturantRepository resturantRepository,
            MenuItemRepository menuItemRepository
            )
        {
            _resturantRepository = resturantRepository;
            _menuItemRepository = menuItemRepository;
        }

        private IItemValidating Build(string json)
        {
            dynamic item = JsonConvert.DeserializeObject<dynamic>(json);

            if (item != null)
            {
                switch (item.type.ToString())
                {
                    case "resturant":
                        return JsonConvert.DeserializeObject<Resturant>(json);
                    case "menuItem":
                        return JsonConvert.DeserializeObject<MenuItem>(json);
                }
            }

            // if no match, return null
            return null;
        }

        public void BuildAndSave(string json)
        {
            IItemValidating item = Build(json);

            if (item.GetType() == typeof(Resturant))
            {
                // This is a resturant
                _resturantRepository.Add((Resturant)item);
            } else
            {
                // This is a menu item
                _menuItemRepository.Add((MenuItem)item);
            }
        }
    }
}
