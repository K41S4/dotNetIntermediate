namespace RESTapp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Item> Items { get; set; }

        public Category()
        {
            Items = new List<Item>();
        }
    }
}
