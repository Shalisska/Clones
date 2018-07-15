using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Adapters;
using Application.Data.Repositories;
using Infrastructure.Data.EF;
using Infrastructure.Data.Entities;

namespace Infrastructure.Data.Repositories
{
    public class StockRepository : IStockRepository
    {
        ClonesContextEF _db;

        public StockRepository(ClonesContextEF context)
        {
            _db = context;
        }

        public IEnumerable<IStockAdapter> GetAll()
        {
            return _db.Stocks.ToList();
        }

        public IStockAdapter Get(int id)
        {
            return _db.Stocks.Find(id);
        }

        public void Create(IStockAdapter item)
        {
            Stock stock = new Stock(item);
            _db.Stocks.Add(stock);
        }

        public void Update(IStockAdapter item)
        {
            Stock stock = new Stock(item);
            _db.Entry(stock).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Stock stock = _db.Stocks.Find(id);
            if (stock != null)
                _db.Stocks.Remove(stock);
        }
    }
}
