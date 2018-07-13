using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Management.Data.Repositories;

namespace Application.Management.Data.UnitsOfWork
{
    public interface IProfileUnitOfWork : IDisposable
    {
        IProfileRepository Profiles { get; }
        IAccountRepository Accounts { get; }

        void Save();
    }
}
