using Application.Adapters;

namespace Application.Models
{
    public class StockModel : IStockAdapter
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public StockModel() { }

        public StockModel(IStockAdapter adapter) : this()
        {
            Id = adapter.Id;
            Name = adapter.Name;
        }
    }
}
