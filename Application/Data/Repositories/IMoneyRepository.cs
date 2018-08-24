using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Adapters;

namespace Application.Data.Repositories
{
    public interface IMoneyRepository : IRepositorySimple<IMoneyAdapter>
    {
    }
}
