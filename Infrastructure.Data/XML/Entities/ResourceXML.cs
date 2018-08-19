using System.Xml.Serialization;

namespace Infrastructure.Data.XML.Entities
{
    public class ResourceXML
    {
        [XmlAttribute("StockId")]
        public int StockId { get; set; }

        public string Name { get; set; }
        public decimal PriceBase { get; set; }
        public decimal Price { get; set; }
    }
}
