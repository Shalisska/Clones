using Application.Adapters;

namespace Application.Data.Repositories
{
    public interface IResourceRepository : IRepositorySimple<IResourceAdapter>
    {
        void UpdateFromXML();
    }
}
