using Application.Data.Repositories;
using Application.Data.UnitsOfWork;
using Infrastructure.Data.EF;
using Infrastructure.Data.Repositories;
using System;

namespace Infrastructure.Data.UnitsOfWork
{
    public class StockUnitOfWork : IStockUnitOfWork
    {
        ClonesContextEF _db;
        StockRepository _stockRepository;

        public StockUnitOfWork(string connectionString)
        {
            _db = new ClonesContextEF(connectionString);
        }

        public IStockRepository Stocks
        {
            get
            {
                if (_stockRepository == null)
                    _stockRepository = new StockRepository(_db);
                return _stockRepository;
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
