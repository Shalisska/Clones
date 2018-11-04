using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Adapters;

namespace Infrastructure.Data.Entities
{
    public class Currency : ICurrencyAdapter
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }

        public int StockId { get; set; }

        public virtual Stock Stock { get; set; }

        public Currency() { }

        public Currency(ICurrencyAdapter adapter)
        {
            Id = adapter.Id;
            Name = adapter.Name;
            BuyPrice = adapter.BuyPrice;
            SellPrice = adapter.SellPrice;
            StockId = adapter.StockId;
        }
    }
}
