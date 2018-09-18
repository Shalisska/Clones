using Application.Adapters;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class ResourceModel : IResourceAdapter
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int StockId { get; set; }
        [Required]
        public string Name { get; set; }

        public StockModel Stock { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true,
               DataFormatString = "{0:0.0000}")]
        public decimal PriceBase { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true,
               DataFormatString = "{0:0.0000}")]
        public decimal Price { get; set; }

        public IStockAdapter StockAdapter { get; set; }

        public ResourceModel() { }

        public ResourceModel(IResourceAdapter adapter)
        {
            Id = adapter.Id;
            StockId = adapter.StockId;
            Name = adapter.Name;
            PriceBase = adapter.PriceBase;
            Price = adapter.Price;
        }
    }
}
