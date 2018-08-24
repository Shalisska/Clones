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
    public class MoneyRepository : IMoneyRepository
    {
        private ClonesContextEF _db;

        public MoneyRepository(ClonesContextEF context)
        {
            _db = context;
        }

        public IEnumerable<IMoneyAdapter> GetAll()
        {
            return _db.Money.ToList();
        }

        public IMoneyAdapter Get(int id)
        {
            return _db.Money.Find(id);
        }

        public void Create(IMoneyAdapter item)
        {
            Money money = new Money(item);
            _db.Money.Add(money);
        }

        public void Update(IMoneyAdapter item)
        {
            Money money = new Money(item);
            _db.Entry(money).State= EntityState.Modified;
        }

        public void Delete(int id)
        {
            Money money = _db.Money.Find(id);
            if (money != null)
                _db.Money.Remove(money);
        }
    }
}
