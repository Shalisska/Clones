using Application.Data.Repositories;
using System;

namespace Application.Data.UnitsOfWork
{
    public interface IResourceUnitOfWork : IDisposable
    {
        IResourceRepository Resources { get; }

        void Save();
    }
}
