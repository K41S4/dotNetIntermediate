using RESTapp.Models;
using RESTapp.Repositories.Interfaces;

namespace RESTapp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        List<Category> _categories;
        IItemRepository _itemRepository;
        public CategoryRepository(IItemRepository itemRepository)
        {
            _categories = new List<Category>();
            _itemRepository = itemRepository;
        }

        public Category AddCategory(Category category)
        {
            _categories.Add(category);
            return category;
        }

        public Category DeleteCategory(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category != null) _categories.Remove(category);
            return category;
        }

        public List<Category> GetCategories()
        {
            return _categories;
        }

        public Category GetCategory(int id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }

        public Category UpdateCategory(int id, Category category)
        {
            var oldCategory = _categories.FirstOrDefault(c => c.Id == id);
            //if (oldCategory != null) _categories.Remove(category);
            return category;
        }
    }
}
