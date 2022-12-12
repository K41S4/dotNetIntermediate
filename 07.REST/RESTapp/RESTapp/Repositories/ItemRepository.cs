using RESTapp.Models;
using RESTapp.Repositories.Interfaces;

namespace RESTapp.Repositories
{
    public class ItemRepository : IItemRepository
    {
        List<Item> _items;
        ICategoryRepository _categoryRepository;
        public ItemRepository(ICategoryRepository categoryRepository)
        {
            _items = new List<Item>();
            _categoryRepository = categoryRepository;
        }
        public Item AddItem(Item item)
        {
            _items.Add(item);
            return item;
        }

        public Item DeleteItem(int id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item != null) _items.Remove(item);
            return item;
        }

        public Item GetItem(int id)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }

        public List<Item> GetItems()
        {
            return _items;
        }

        public Item UpdateItem(int id, Item item)
        {
            throw new NotImplementedException();
        }
    }
}
