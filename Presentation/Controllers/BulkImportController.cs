using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Factory;
using Presentation.Helpers;
using System.IO.Compression;
using System.Text.Json;

namespace Presentation.Controllers
{
    public class BulkImportController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BulkImport(string rawJson, [FromKeyedServices("memory")] IItemsRepository itemsInMemoryRepository)
        {
            ImportItemFactory factory = new ImportItemFactory();

            List<IItemValidating> items = factory.Create(rawJson);

            List<string> importIds = new List<string>();

            foreach (IItemValidating item in items)
            {
                itemsInMemoryRepository.Save(item);

                if (item is MenuItem menuItem)
                {
                    importIds.Add(menuItem.ImportId);
                }
                else if (item is Resturant resturant)
                {
                    importIds.Add(resturant.ImportId);
                }
            }

            string outName = $"wwwroot\\gen\\{Guid.NewGuid()}.zip";
            Compression.MakeZipFile(importIds, outName);

            string webPath = outName.Replace("wwwroot\\gen\\", "/gen/");

            ViewData["zipPath"] = webPath;
            return View("Commit");
        }

        [HttpPost]
        public IActionResult Commit(IFormFile zipFile, [FromKeyedServices("memory")] IItemsRepository itemsInMemoryRepository, [FromKeyedServices("db")] IItemsRepository itemsInDatabaseRepository)
        {
            Dictionary<string, Guid> idToGuid = Compression.SaveAndRetrieveImageMap(zipFile);

            List<Resturant> resturants = itemsInMemoryRepository.GetResturants().ToList();
            Dictionary<string, Resturant> importIdToResturant = new Dictionary<string, Resturant>();

            foreach (Resturant resturant in resturants)
            {
                resturant.Id = 0;

                resturant.ImagePath = $"/imgs/{idToGuid[resturant.ImportId]}.jpg";

                importIdToResturant[resturant.ImportId] = resturant;

                itemsInDatabaseRepository.Save(resturant);
            }

            List<MenuItem> menuItems = itemsInMemoryRepository.GetMenuItems().ToList();

            foreach (MenuItem menuItem in menuItems)
            {
                menuItem.Id = Guid.NewGuid();
                menuItem.ResturantFK = importIdToResturant[menuItem.ImportFK].Id;

                menuItem.ImagePath = $"/imgs/{idToGuid[menuItem.ImportId]}.jpg";

                itemsInDatabaseRepository.Save(menuItem);
            }

            // wipe in memory db
            itemsInMemoryRepository.DeleteAll();

            return Ok();
        }
    }
}

