using Application.Adapters;

namespace Infrastructure.Data.Entities
{
    public class Stock : IStockAdapter
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Stock() { }

        public Stock(IStockAdapter adapter) : this()
        {
            Id = adapter.Id;
            Name = adapter.Name;
        }
    }
}
