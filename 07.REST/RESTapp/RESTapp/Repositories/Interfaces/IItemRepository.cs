using RESTapp.Models;

namespace RESTapp.Repositories.Interfaces
{
    public interface IItemRepository
    {
        List<Item> GetItems();
        Item GetItem(int id);
        Item AddItem(Item item);
        Item UpdateItem(int id, Item item);
        Item DeleteItem(int id);
    }
}
