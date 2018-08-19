using Application.Adapters;
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

        public StockModel() { }

        public StockModel(IStockAdapter adapter) : this()
        {
            Id = adapter.Id;
            Name = adapter.Name;
        }
    }
}
