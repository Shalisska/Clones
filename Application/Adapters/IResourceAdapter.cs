namespace Application.Adapters
{
    public interface IResourceAdapter
    {
        int Id { get; }
        int StockId { get; }

        string Name { get; }

        decimal PriceBase { get; }
        decimal Price { get; }
    }
}
