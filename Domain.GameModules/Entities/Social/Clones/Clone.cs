using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.EconomicalBaseConcepts;
using Domain.GameModules.Resources;

namespace Domain.GameModules.Entities.Social.Clones
{
    public class Clone : IEconomicalUnit
    {
        public string Name { get; set; }
        public SocialStatus Status { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int Age { get; set; }

        public IEnumerable<Characteristic> Characteristics { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<MilitarySkill> MilitarySkills { get; set; }

        public Tool Tool { get; set; }
        public Weapon Weapon { get; set; }
    }
}
