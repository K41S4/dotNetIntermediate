using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using RESTapp.Models;
using RESTapp.Services;
using RESTapp.Services.Interfaces;

namespace RESTapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }
        
        [HttpGet]
        public IEnumerable<Item> FilteredItems([FromQuery]int? page, [FromQuery] int? categoryId)
        {
            var filteredItems = _itemService.GetItems();
            if (page != null) filteredItems = _itemService.FilterByPagination(page.Value, _itemService.GetItems());
            if (categoryId != null) filteredItems = _itemService.FilterByCategory(categoryId.Value, filteredItems);
            return filteredItems;
        }

        [HttpPost]
        public Item Create([FromBody] Item item)
        {
            return _itemService.AddItem(item);
        }

        [HttpPut("{id}")]
        public Item Put([FromBody] Item item, int id)
        {
            return _itemService.UpdateItem(id, item);
        }

        [HttpDelete("{id}")]
        public Item Delete(int id)
        {
            return _itemService.DeleteItem(id);
        }
    }
}
