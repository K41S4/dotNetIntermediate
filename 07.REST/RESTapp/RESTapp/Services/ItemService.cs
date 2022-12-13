using RESTapp.Models;
using RESTapp.Repositories.Interfaces;
using RESTapp.Services.Interfaces;

namespace RESTapp.Services
{
    public class ItemService : IItemService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;
        public ItemService(ICategoryRepository categoryRepository, IItemRepository itemRepository)
        {
            _categoryRepository = categoryRepository;
            _itemRepository = itemRepository;
        }

        public Item AddItem(Item item)
        {
            _categoryRepository.AddItem(item.CategoryId, item);
            return _itemRepository.AddItem(item);
        }

        public Item DeleteItem(int id)
        {
            var deletedItem = _itemRepository.DeleteItem(id);
            _categoryRepository.DeleteItem(deletedItem.CategoryId, deletedItem);
            return deletedItem;
        }

        public List<Item> GetItems()
        {
            return _itemRepository.GetItems();
        }

        public Item UpdateItem(int id, Item item)
        {
            var oldItem = _itemRepository.GetItem(id);
            _categoryRepository.DeleteItem(oldItem.CategoryId, oldItem);
            _categoryRepository.AddItem(item.CategoryId, item);
            return _itemRepository.UpdateItem(id, item);
        }

        public List<Item> FilterByPagination(int page, List<Item> items)
        {
            if (page * 2 + 2 <= items.Count) return items.GetRange(page * 2, 2);
            else return items.GetRange(page * 2, items.Count - page * 2);
        }

        public List<Item> FilterByCategory(int categoryId, List<Item> items)
        {
            return items.Where(w => w.CategoryId == categoryId).ToList();
        }
    }
}
