using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.EconomicalBaseConcepts;

namespace Domain.GameModules.Resources
{
    public class Tool : IEconomicalResource
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public decimal BasePrice { get; set; }

        public decimal CostPrice { get; set; }

        /// <summary>
        /// Прочночть
        /// </summary>
        public int BaseSrength { get; set; }

        public int CurrentSrength { get; set; }

        /// <summary>
        /// Расход прочности
        /// </summary>
        public decimal SrengthConsumption { get; set; }
    }
}
