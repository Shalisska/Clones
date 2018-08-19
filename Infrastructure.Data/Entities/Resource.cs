using Application.Adapters;

namespace Infrastructure.Data.Entities
{
    public class Resource : IResourceAdapter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PriceBase { get; set; }
        public decimal Price { get; set; }

        public int StockId { get; set; }

        public virtual Stock Stock { get; set; }

        public Resource() { }

        public Resource(IResourceAdapter adapter) : this()
        {
            Id = adapter.Id;
            Name = adapter.Name;
            PriceBase = adapter.PriceBase;
            Price = adapter.Price;

            StockId = adapter.StockId;
        }
    }
}
