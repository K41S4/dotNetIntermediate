using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTapp.Models;
using RESTapp.Services;
using RESTapp.Services.Interfaces;

namespace RESTapp.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IEnumerable<Category> Categories()
        {
            return _categoryService.GetCategories();
        }

        [HttpPost]
        public Category Create([FromBody]Category category)
        {
            return _categoryService.AddCategory(category);
        }

        [HttpPut("{id}")]
        public Category Put([FromBody]Category category, int id)
        {
            return _categoryService.UpdateCategory(id, category);
        }

        [HttpDelete("{id}")]
        public Category Delete(int id)
        {
            return _categoryService.DeleteCategory(id);
        }
    }
}
