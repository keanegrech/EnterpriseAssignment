using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace Presentation.Factory
{
    public class ImportItemFactory
    {
        public List<IItemValidating> Create(string json)
        {
            dynamic items = JsonConvert.DeserializeObject<dynamic>(json);

            if (items != null)
            {
                List<IItemValidating> importedItems = new List<IItemValidating>();

                // restaurants
                foreach (var obj in items)
                {
                    if (obj.type == "restaurant")
                    {
                        var restaurantJson = JsonConvert.DeserializeObject(obj.ToString());

                        Resturant resturant = new Resturant
                        {
                            Name = restaurantJson.name,
                            Description = restaurantJson.description,
                            OwnerEmailAddress = restaurantJson.ownerEmailAddress,
                            Address = restaurantJson.address,
                            Status = "pending",
                            ImportId = restaurantJson.id
                        };

                        importedItems.Add(resturant);
                    }
                }

                foreach (var obj in items)
                {
                    if (obj.type == "menuItem")
                    {
                        var menuItemJson = JsonConvert.DeserializeObject(obj.ToString());

                        MenuItem menuItem = new MenuItem
                        {
                            Id = Guid.NewGuid(),
                            Title = menuItemJson.title,
                            Price = menuItemJson.price,
                            Status = "pending",
                            ImportId = menuItemJson.id,
                            ImportFK = menuItemJson.restaurantId
                        };

                        importedItems.Add(menuItem);
                    }
                }

                return importedItems;
            }

            return null;
        }
    }
}
