using Application.Data.UnitsOfWork;
using Application.Management.Interfaces;
using Application.Models;
using System.Collections.Generic;

namespace Application.Management
{
    public class ResourceManagementService : IResourceManagementService
    {
        IResourceUnitOfWork _resourceUOW;

        public ResourceManagementService(IResourceUnitOfWork resourceUOW)
        {
            _resourceUOW = resourceUOW;
        }

        public IEnumerable<ResourceModel> GetResources()
        {
            var resources = _resourceUOW.Resources.GetAll();
            List<ResourceModel> models = new List<ResourceModel>();

            if (resources != null)
                foreach (var item in resources)
                    models.Add(new ResourceModel(item));

            return models;
        }

        public void CreateResource(ResourceModel resource)
        {
            _resourceUOW.Resources.Create(resource);
            _resourceUOW.Save();
        }

        public void UpdateResources(IEnumerable<ResourceModel> resources)
        {
            foreach (var resource in resources)
                _resourceUOW.Resources.Update(resource);
            _resourceUOW.Save();
        }

        public void DeleteResources(IEnumerable<int> ids)
        {
            foreach (var id in ids)
                _resourceUOW.Resources.Delete(id);
            _resourceUOW.Save();
        }

        public void UpdateFromXML()
        {
            _resourceUOW.Resources.UpdateFromXML();
            _resourceUOW.Save();
        }

        public void Dispose()
        {
            _resourceUOW.Dispose();
        }
    }
}
