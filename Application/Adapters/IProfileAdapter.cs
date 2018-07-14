using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Adapters
{
    public interface IProfileAdapter
    {
        int Id { get; }
        string Name { get; }

        IEnumerable<IAccountAdapter> AccountAdapters { get; }
    }
}
