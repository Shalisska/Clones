using Domain.Core.EconomicalBaseConcepts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.GameModules.Resources
{
    /// <summary>
    /// Денежные ресурсы
    /// </summary>
    public class Currency : ICurrency
    {

        public string Name { get; set; }

        public bool IsBaseCurrency { get; set; }

        public decimal BasePrice { get; set; }

        public decimal CostPrice { get; set; }

        public decimal ExchangeRate { get; set; }

        public decimal Value { get; set; }
    }
}
