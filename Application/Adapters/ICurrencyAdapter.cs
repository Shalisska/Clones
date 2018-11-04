using System.Collections.Generic;

namespace Application.Adapters
{
    public interface ICurrencyAdapter
    {
        int Id { get; }
        string Name { get; }

        decimal BuyPrice { get; set; }
        decimal SellPrice { get; set; }

        int StockId { get; }
    }
}
