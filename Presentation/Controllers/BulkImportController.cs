using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Factory;
using System.Text.Json;

namespace Presentation.Controllers
{
    public class BulkImportController : Controller
    {
        [HttpPost]
        public IActionResult Index([FromBody] JsonElement json, [FromKeyedServices("memory")] IItemsRepository itemsInMemoryRepository)
        {
            string rawJson = json.GetRawText();

            ImportItemFactory factory = new ImportItemFactory(itemsInMemoryRepository);

            factory.Create(rawJson);

            return Ok();
        }
    }
}
