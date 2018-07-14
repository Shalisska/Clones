using Application.Data.Repositories;
using Application.Models;
using Infrastructure.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Adapters;
using Infrastructure.Data.Entities;
using System.Data.Entity;

namespace Infrastructure.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private ClonesContextEF _db;

        public AccountRepository(ClonesContextEF context)
        {
            _db = context;
        }

        public IEnumerable<IAccountAdapter> GetAll()
        {
            return _db.Accounts.ToList();
        }

        public IAccountAdapter Get(int id)
        {
            return _db.Accounts.Find(id);
        }

        public void Create(IAccountAdapter item)
        {
            _db.Accounts.Add(new Account(item));
        }

        public void Update(IAccountAdapter item)
        {
            _db.Entry(new Account(item)).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var account = _db.Accounts.Find(id);
            if (account != null)
                _db.Accounts.Remove(account);
        }
    }
}
