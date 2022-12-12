using RESTapp.Models;
using RESTapp.Repositories.Interfaces;
using RESTapp.Services.Interfaces;

namespace RESTapp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;
        public CategoryService(ICategoryRepository categoryRepository, IItemRepository itemRepository)
        {
            _categoryRepository = categoryRepository;
            _itemRepository = itemRepository;
        }

        public Category AddCategory(Category category)
        {
            return _categoryRepository.AddCategory(category);
        }

        public Category DeleteCategory(int id)
        {
            var deletedCategory = _categoryRepository.DeleteCategory(id);
            foreach(var item in deletedCategory.Items)
            {
                _itemRepository.DeleteItem(item.Id);
            }

            return deletedCategory;
        }

        public List<Category> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }

        public Category UpdateCategory(int id, Category category)
        {
            return _categoryRepository.UpdateCategory(id, category);
        }
    }
}
