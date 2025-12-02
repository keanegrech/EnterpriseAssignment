using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Factory;
using Presentation.Helpers;
using System.Text.Json;

namespace Presentation.Controllers
{
    public class BulkImportController : Controller
    {
        [HttpPost]
        public IActionResult BulkImport([FromBody] JsonElement json, [FromKeyedServices("memory")] IItemsRepository itemsInMemoryRepository)
        {
            ImportItemFactory factory = new ImportItemFactory();

            string rawJson = json.GetRawText();

            List<IItemValidating> items = factory.Create(rawJson);

            List<string> importIds = new List<string>();

            foreach (IItemValidating item in items)
            {
                itemsInMemoryRepository.Save(item);

                if (item is MenuItem menuItem)
                    importIds.Add(menuItem.ImportId);
                else if (item is Resturant resturant)
                    importIds.Add(resturant.ImportId);
            }

            string outName = $"wwwroot\\gen\\{Guid.NewGuid()}.zip";
            Compression.MakeZipFile(importIds, outName);

            return Ok();
        }
    }
}
