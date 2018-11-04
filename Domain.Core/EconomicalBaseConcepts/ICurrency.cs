using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.EconomicalBaseConcepts
{
    /// <summary>
    /// Денежные ресурсы
    /// </summary>
    public interface ICurrency : IEconomicalResource
    {
        /// <summary>
        /// Курс
        /// </summary>
        decimal ExchangeRate { get; }

        /// <summary>
        /// Является ли базовой расчетной единицей
        /// </summary>
        bool IsBaseCurrency { get; }
    }
}
