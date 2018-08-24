using System.Collections.Generic;

namespace Application.Adapters
{
    public interface IMoneyAdapter
    {
        int Id { get; }
        string Name { get; }

        decimal BuyPrice { get; set; }
        decimal SellPrice { get; set; }

        int StockId { get; }
    }
}
