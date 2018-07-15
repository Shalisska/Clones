using Application.Data.Repositories;
using System;

namespace Application.Data.UnitsOfWork
{
    public interface IStockUnitOfWork : IDisposable
    {
        IStockRepository Stocks { get; }

        void Save();
    }
}
