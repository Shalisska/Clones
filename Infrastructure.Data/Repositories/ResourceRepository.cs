using Application.Adapters;
using Application.Data.Repositories;
using Application.Data;
using Infrastructure.Data.EF;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Helpers;
using Infrastructure.Data.XML.Entities;
using Infrastructure.Data.XML.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private ClonesContextEF _db;
        private ResourceRepositoryXML _repositoryXML;

        public ResourceRepository(ClonesContextEF contextEF)
        {
            _db = contextEF;
            _repositoryXML = new ResourceRepositoryXML(@"D:\projects\SeleniumTest\SeleniumTest\bin\Debug\Resources.xml");
        }

        public IEnumerable<IResourceAdapter> GetAll()
        {
            return _db.Resources.ToList();
        }

        public IEnumerable<IResourceAdapter> GetAllWithParameters(TableQueryParameters parameters)
        {
            var tableName = "Resources";

            var specialConditions = new List<ConditionsForFK>()
            {
                new ConditionsForFK(
                "StockId",
                " join Stocks on Stocks.Id=Resources.StockId",
                "Stocks.Name")
            };

            var queryBuilder = new QueryBuilder<Resource>(tableName, parameters, specialConditions);
            var query = queryBuilder.GetQueryWithParameters();
            return _db.Resources.SqlQuery(query);
        }

        public IResourceAdapter Get(int id)
        {
            return _db.Resources.Find(id);
        }

        public void Create(IResourceAdapter item)
        {
            _db.Resources.Add(new Resource(item));
        }

        public void Update(IResourceAdapter item)
        {
            _db.Entry(new Resource(item)).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var resource = _db.Resources.Find(id);
            if (resource != null)
                _db.Resources.Remove(resource);
        }

        public void UpdateFromXML()
        {
            IEnumerable<ResourceXML> resourcesXML = _repositoryXML.GetResourcesFromXML();
            IEnumerable<Resource> resources = _db.Resources.ToList();

            foreach(var item in resourcesXML)
            {
                var resource = resources.Where(r => r.Name.Trim() == item.Name).FirstOrDefault();

                if (resource == null)
                {
                    var newResource = new Resource()
                    {
                        Name = item.Name,
                        PriceBase=item.PriceBase,
                        Price=item.Price,
                        StockId=item.StockId
                    };
                    _db.Resources.Add(newResource);
                }
                else
                {
                    resource.Price = item.Price;
                    _db.Entry(resource).State = EntityState.Modified;
                }
            }
        }
    }
}
