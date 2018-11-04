using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Adapters;

namespace Application.Models
{
    public class CurrencyModel : ICurrencyAdapter
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }

        public int StockId { get; set; }

        public CurrencyModel() { }

        public CurrencyModel(ICurrencyAdapter adapter)
        {
            Id = adapter.Id;
            Name = adapter.Name;
            BuyPrice = adapter.BuyPrice;
            SellPrice = adapter.SellPrice;
            StockId = adapter.StockId;
        }
    }
}
