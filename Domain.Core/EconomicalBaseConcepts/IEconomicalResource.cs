namespace Domain.Core.EconomicalBaseConcepts
{
    /// <summary>
    /// Экономический ресурс
    /// </summary>
    public interface IEconomicalResource
    {
        /// <summary>
        /// Базовая стоимость
        /// </summary>
        decimal BasePrice { get; }

        /// <summary>
        /// Себестоимость
        /// </summary>
        decimal CostPrice { get; }
    }
}
