using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.EconomicalBaseConcepts;

namespace Domain.GameModules.Entities.Social.Clones
{
    public class MilitarySkill : IEconomicalResource
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public decimal BasePrice { get; set; }

        public decimal CostPrice { get; set; }
    }
}
