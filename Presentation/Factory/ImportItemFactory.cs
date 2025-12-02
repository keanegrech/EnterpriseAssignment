using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json;

namespace Presentation.Factory
{
    public class ImportItemFactory
    {
        private IItemsRepository _itemsInMemoryRepository;

        public ImportItemFactory(IItemsRepository itemsInMemoryRepository)
        {
            _itemsInMemoryRepository = itemsInMemoryRepository;
        }

        public List<IItemValidating> Create(string json)
        {
            return null;
        }
    }
}
