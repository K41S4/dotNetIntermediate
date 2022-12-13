using Bogus;
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

            var faker = new Faker<Category>()
                .RuleFor(u => u.Id, f => f.IndexFaker)
                .RuleFor(u => u.Name, f => f.Name.FirstName());
            _categories = faker.Generate(5);
            foreach (var item in _categories)
            {
                item.Items = _itemRepository.GetItems().Where(w => w.CategoryId == item.Id).ToList();
            }
        }

        public Category AddCategory(Category category)
        {
            _categories.Add(category);
            return category;
        }

        public void AddItem(int categoryId, Item item)
        {
            var category = _categories.FirstOrDefault(w => w.Id == categoryId);
            if(category != null) category.Items.Add(item);
        }

        public void DeleteItem(int categoryId, Item item)
        {
            var category = _categories.FirstOrDefault(w => w.Id == categoryId);
            if(category != null) category.Items.Remove(item);
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
            if (oldCategory != null)
            {
                oldCategory.Name = category.Name;
            }
            return category;
        }
    }
}
