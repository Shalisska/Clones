using Application.Management.Models;
using Application.Management.Adapters;

namespace Application.Management.Data.Repositories
{
    public interface IProfileRepository : IRepositorySimple<IProfileAdapter>
    {
    }
}
