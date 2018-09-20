namespace Domain.Core.EconomicalBaseNotions
{
    /// <summary>
    /// Транзакция
    /// </summary>
    public interface ITransaction
    {
        /// <summary>
        /// Приходная часть транзакции
        /// </summary>
        ITransactionPart IncomePart { get; }

        /// <summary>
        /// Расходная часть транзакции
        /// </summary>
        ITransactionPart OutcomePart { get; }

        /// <summary>
        /// Стоимость единицы приходного ресурса
        /// </summary>
        decimal PricePerOne { get; }

        /// <summary>
        /// Общая стоимость за приход
        /// </summary>
        decimal TotalPrice { get; }
    }
}
