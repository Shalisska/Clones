using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Management.Data.UnitsOfWork;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.EF;
using Application.Management.Data.Repositories;

namespace Infrastructure.Data.UnitsOfWork
{
    public class ProfileUnitOfWork : IProfileUnitOfWork
    {
        private ClonesContextEF _db;
        private ProfileRepository _profileRepository;
        private AccountRepository _accountRepository;

        public ProfileUnitOfWork(string connectionString)
        {
            _db = new ClonesContextEF(connectionString);
        }

        public IProfileRepository Profiles
        {
            get
            {
                if (_profileRepository == null)
                    _profileRepository = new ProfileRepository(_db);
                return _profileRepository;
            }
        }

        public IAccountRepository Accounts
        {
            get
            {
                if (_accountRepository == null)
                    _accountRepository = new AccountRepository(_db);
                return _accountRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _db.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
