using Application.Models;
using System.Collections.Generic;

namespace Application.Management.Interfaces
{
    public interface IResourceManagementService
    {
        IEnumerable<ResourceModel> GetResources();

        void CreateResource(ResourceModel resource);
        void UpdateResources(IEnumerable<ResourceModel> resources);

        void DeleteResources(IEnumerable<int> ids);

        void UpdateFromXML();

        void Dispose();
    }
}
