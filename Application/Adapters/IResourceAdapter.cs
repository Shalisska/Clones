namespace Application.Adapters
{
    public interface IResourceAdapter
    {
        int Id { get; }
        int StockId { get; }
        IStockAdapter StockAdapter { get; }

        string Name { get; }

        decimal PriceBase { get; }
        decimal Price { get; }
    }
}
