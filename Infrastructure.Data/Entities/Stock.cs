using System.Collections.Generic;
using Application.Adapters;

namespace Infrastructure.Data.Entities
{
    public class Stock : IStockAdapter
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<IResourceAdapter> ResourceAdapters { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }

        public Stock()
        {
            Resources = new HashSet<Resource>();
        }

        public Stock(IStockAdapter adapter) : this()
        {
            Id = adapter.Id;
            Name = adapter.Name;

            Resources = new List<Resource>();

            if (adapter.ResourceAdapters != null)
                foreach (var item in adapter.ResourceAdapters)
                    Resources.Add(new Resource(item));
        }
    }
}
