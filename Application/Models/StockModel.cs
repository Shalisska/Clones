using Application.Adapters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class StockModel : IStockAdapter
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Column name")]
        public string Name { get; set; }
        public List<ResourceModel> Resources { get; set; }

        public IEnumerable<IResourceAdapter> ResourceAdapters { get; set; }

        public StockModel() { }

        public StockModel(IStockAdapter adapter) : this()
        {
            Id = adapter.Id;
            Name = adapter.Name;
            Resources = new List<ResourceModel>();

            if (adapter.ResourceAdapters != null)
                foreach (var item in adapter.ResourceAdapters)
                    Resources.Add(new ResourceModel(item));
        }
    }
}
