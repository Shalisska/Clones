using Application.Models;
using Application.Adapters;

namespace Application.Data.Repositories
{
    public interface IProfileRepository : IRepositorySimple<IProfileAdapter>
    {
    }
}
