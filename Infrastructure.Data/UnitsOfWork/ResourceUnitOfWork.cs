using Application.Data.Repositories;
using Application.Data.UnitsOfWork;
using Infrastructure.Data.EF;
using Infrastructure.Data.Repositories;
using System;

namespace Infrastructure.Data.UnitsOfWork
{
    public class ResourceUnitOfWork : IResourceUnitOfWork
    {
        private ClonesContextEF _db;
        private ResourceRepository _resourceRepository;

        public ResourceUnitOfWork(string connectionString)
        {
            _db = new ClonesContextEF(connectionString);
        }

        public IResourceRepository Resources
        {
            get
            {
                if (_resourceRepository == null)
                    _resourceRepository = new ResourceRepository(_db);
                return _resourceRepository;
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
