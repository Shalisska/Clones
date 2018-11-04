using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.EconomicalBaseConcepts;

namespace Domain.GameModules.Entities
{
    public class Account : IGeneralAccount
    {
        public IEnumerable<IEconomicalModule> EconomicalModules { get; set; }

        public decimal TotalCost { get; set; }

        public decimal CurrencyTotalCost { get; set; }

        public decimal ResourcesTotalCost { get; set; }

        public IEnumerable<ICurrency> CurrencyAccounts { get; set; }

        public IEnumerable<IEconomicalResource> Resources { get; set; }
    }
}
