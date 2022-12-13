using RESTapp.Models;

namespace RESTapp.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategory(int id);
        Category AddCategory(Category category);
        Category UpdateCategory(int id, Category category);
        Category DeleteCategory(int id);
        void AddItem(int categoryId, Item item);
        void DeleteItem(int categoryId, Item item);
    }
}
