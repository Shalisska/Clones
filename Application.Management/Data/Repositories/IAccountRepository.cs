using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Management.Models;
using Application.Management.Adapters;

namespace Application.Management.Data.Repositories
{
    public interface IAccountRepository : IRepositorySimple<IAccountAdapter>
    {
    }
}
