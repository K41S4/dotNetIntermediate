using RESTapp.Models;

namespace RESTapp.Services.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetCategories();  
        Category AddCategory(Category category);  
        Category UpdateCategory(int id, Category category);  
        Category DeleteCategory(int id);  
    }
}
