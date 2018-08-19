using System.Collections.Generic;

namespace Application.Adapters
{
    public interface IStockAdapter
    {
        int Id { get; }
        string Name { get; }

        IEnumerable<IResourceAdapter> ResourceAdapters { get; }
    }
}
