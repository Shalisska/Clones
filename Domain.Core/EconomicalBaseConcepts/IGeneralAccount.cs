using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.EconomicalBaseConcepts
{
    /// <summary>
    /// Обобщающая информация обо всех операциях, содержит информацию по итоговым счетам и общей оценке всей деятельности
    /// </summary>
    public interface IGeneralAccount
    {
        /// <summary>
        /// Общая стоимость аккаунта
        /// </summary>
        decimal TotalCost { get; }

        /// <summary>
        /// Итоговая стоимость всех денежных счетов
        /// </summary>
        decimal CurrencyTotalCost { get; }

        /// <summary>
        /// Итоговая стоимость ресурсов
        /// </summary>
        decimal ResourcesTotalCost { get; }

        /// <summary>
        /// Денежные счета
        /// </summary>
        IEnumerable<ICurrency> CurrencyAccounts { get; }

        /// <summary>
        /// Ресурсы
        /// </summary>
        IEnumerable<IEconomicalResource> Resources { get; }

        /// <summary>
        /// Модули
        /// </summary>
        IEnumerable<IEconomicalModule> EconomicalModules { get; }
    }
}
