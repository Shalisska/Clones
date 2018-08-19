using Infrastructure.Data.XML.Entities;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Infrastructure.Data.XML.Repositories
{
    public class ResourceRepositoryXML
    {
        private string _connectionString;
        XmlSerializer _formatter = new XmlSerializer(typeof(ResourceXML[]));

        public ResourceRepositoryXML(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ResourceXML> GetResourcesFromXML()
        {
            using (FileStream fs = new FileStream(_connectionString, FileMode.OpenOrCreate))
            {
                ResourceXML[] resources = (ResourceXML[])_formatter.Deserialize(fs);
                return resources;
            }
        }
    }
}
