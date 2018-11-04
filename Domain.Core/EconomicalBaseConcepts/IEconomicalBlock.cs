using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.EconomicalBaseConcepts
{
    /// <summary>
    /// Сущность, описывающая конкретную часть раздела экономической деятельности
    /// Включает в себя набор более мелких элементов
    /// Содержит информацию и потребностях и возможностях входящих элементов
    /// </summary>
    public interface IEconomicalBlock
    {
        IEnumerable<IEconomicalUnit> EconomicalUnits { get; }
    }
}
