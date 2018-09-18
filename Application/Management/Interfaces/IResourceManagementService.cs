using Application.Models;
using Application.Data;
using System.Collections.Generic;

namespace Application.Management.Interfaces
{
    public interface IResourceManagementService
    {
        IEnumerable<ResourceModel> GetResources();
        IEnumerable<ResourceModel> GetParametricalResources(TableQueryParameters parameters);

        void CreateResource(ResourceModel resource);
        void UpdateResources(IEnumerable<ResourceModel> resources);

        void DeleteResources(IEnumerable<int> ids);

        void UpdateFromXML();

        void Dispose();
    }
}
