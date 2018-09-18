using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class TableQueryParameters
    {
        public TableQueryParameters() { }

        public IDictionary<string, string> Sort { get; set; }

        public TableQueryParametersFilters Filters { get; set; }
    }

    public class TableQueryParametersFilters
    {
        public TableQueryParametersFilters() { }

        public IDictionary<string, IEnumerable<string>> ByName { get; set; }
    }
}
