using Application.Adapters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Application.Data;

namespace Application.Data.Repositories
{
    public interface IResourceRepository : IRepositorySimple<IResourceAdapter>
    {
        IEnumerable<IResourceAdapter> GetAllWithParameters(TableQueryParameters parameters);

        void UpdateFromXML();
    }
}
