namespace Domain.Core.EconomicalBaseNotions
{
    /// <summary>
    /// Часть транзакции
    /// </summary>
    public interface ITransactionPart
    {
        /// <summary>
        /// Ресурс
        /// </summary>
        IConvertableResource Resource { get; }

        /// <summary>
        /// Количество
        /// </summary>
        decimal Value { get; }
    }
}
