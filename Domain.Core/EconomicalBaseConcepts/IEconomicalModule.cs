using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.EconomicalBaseConcepts
{
    /// <summary>
    /// Экономическая сущность, состоящая из экономических блоков и описывающая потребности и возможности конкретного раздела экономической деятельности.
    /// Имеет возможность создавать внешние производственные цепочки, осуществляет внешнее взаимодействие с другими модулями и имеет доступ к общим ресурсам (GeneralAccount)
    /// Содержит информацию об общих потребностях и возможностях модуля, аккумулирует и распределяет ресурсы меджу входящими в ее состав блоками
    /// Обобщает информацию о потребностях и возможностях блоков
    /// </summary>
    public interface IEconomicalModule
    {
        IEnumerable<IEconomicalBlock> EconomicalBlocks { get; }
    }
}
