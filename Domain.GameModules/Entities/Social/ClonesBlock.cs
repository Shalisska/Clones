using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.EconomicalBaseConcepts;

namespace Domain.GameModules.Entities.Social
{
    public class ClonesBlock : IEconomicalBlock
    {
        public IEnumerable<IEconomicalUnit> EconomicalUnits { get; set; }
    }
}
