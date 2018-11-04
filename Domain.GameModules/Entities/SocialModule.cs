using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.EconomicalBaseConcepts;

namespace Domain.GameModules.Entities
{
    public class SocialModule : IEconomicalModule
    {
        public IEnumerable<IEconomicalBlock> EconomicalBlocks { get; set; }
    }
}
