namespace Domain.Core.EconomicalBaseConcepts
{
    /// <summary>
    /// Экономический ресурс
    /// </summary>
    public interface IEconomicalResource
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }

        decimal Value { get; }

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
