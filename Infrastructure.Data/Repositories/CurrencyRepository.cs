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
    public class CurrencyRepository : ICurrencyRepository
    {
        private ClonesContextEF _db;

        public CurrencyRepository(ClonesContextEF context)
        {
            _db = context;
        }

        public IEnumerable<ICurrencyAdapter> GetAll()
        {
            return _db.Money.ToList();
        }

        public ICurrencyAdapter Get(int id)
        {
            return _db.Money.Find(id);
        }

        public void Create(ICurrencyAdapter item)
        {
            Currency money = new Currency(item);
            _db.Money.Add(money);
        }

        public void Update(ICurrencyAdapter item)
        {
            Currency money = new Currency(item);
            _db.Entry(money).State= EntityState.Modified;
        }

        public void Delete(int id)
        {
            Currency money = _db.Money.Find(id);
            if (money != null)
                _db.Money.Remove(money);
        }
    }
}
