﻿namespace Domain.Core.EconomicalBaseConcepts
{
    /// <summary>
    /// Часть транзакции
    /// </summary>
    public interface ITransactionPart
    {
        /// <summary>
        /// Ресурс
        /// </summary>
        IEconomicalResource Resource { get; }

        /// <summary>
        /// Количество
        /// </summary>
        decimal Value { get; }
    }
}
