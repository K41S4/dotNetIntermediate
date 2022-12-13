using RESTapp.Models;

namespace RESTapp.Services.Interfaces
{
    public interface IItemService
    {
        List<Item> GetItems();
        Item AddItem(Item Item);
        Item UpdateItem(int id, Item Item);
        Item DeleteItem(int id);
        List<Item> FilterByPagination(int page, List<Item> items);
        List<Item> FilterByCategory(int categoryId, List<Item> items);
    }
}
