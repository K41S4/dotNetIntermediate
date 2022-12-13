using Bogus;
using RESTapp.Models;
using RESTapp.Repositories.Interfaces;

namespace RESTapp.Repositories
{
    public class ItemRepository : IItemRepository
    {
        List<Item> _items;
        public ItemRepository()
        {
            _items = new List<Item>();
            var faker = new Faker<Item>()
               .RuleFor(u => u.Id, f => f.IndexGlobal)
               .RuleFor(u => u.Name, f => f.Name.FirstName())
               .RuleFor(u => u.CategoryId, f => f.Random.Int(0, 4));
            _items = faker.Generate(5);
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
            var oldItem= _items.FirstOrDefault(c => c.Id == id);
            if (oldItem != null)
            {
                oldItem.Name = oldItem.Name;
                oldItem.CategoryId = oldItem.CategoryId;
            }
            return item;
        }
    }
}
