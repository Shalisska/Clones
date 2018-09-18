using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data;

namespace Infrastructure.Data.Helpers
{
    public class QueryBuilder<T>
        where T : class
    {
        public QueryBuilder(
            string tableName,
            TableQueryParameters parameters,
            IEnumerable<ConditionsForFK> specialConditions)
        {
            TableName = tableName;
            Parameters = parameters;
            SpecialConditions = specialConditions;

            ValidKeys = ValidateParameters();
            ConditionsProperties = new HashSet<string>(SpecialConditions.Select(c => c.PropertyName));
        }

        public string TableName { get; set; }
        public TableQueryParameters Parameters { get; set; }
        public IEnumerable<ConditionsForFK> SpecialConditions { get; set; }

        HashSet<string> ValidKeys;
        HashSet<string> ConditionsProperties;

        public string GetQueryWithParameters()
        {
            string tablePart = GetTableQueryPart();
            string queryFiltersByName = GetFiltersByNameQueryPart();
            string queryOrder = GetSortOrdersQueryPart();

            string query = @"select * from " + tablePart + queryFiltersByName + queryOrder;

            //System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@name", "%Samsung%");
            //var phones = db.Database.SqlQuery<Phone>("SELECT * FROM Phones WHERE Name LIKE @name", param);

            return query;
        }

        private HashSet<string> ValidateParameters()
        {
            var properties = typeof(T).GetProperties();
            HashSet<string> allKeys = new HashSet<string>();
            HashSet<string> validKeys = new HashSet<string>();

            if (Parameters?.Sort != null)
                foreach (var item in Parameters.Sort)
                    allKeys.Add(item.Key);

            if (Parameters?.Filters?.ByName != null)
                foreach (var item in Parameters.Filters.ByName)
                    allKeys.Add(item.Key);

            foreach (var key in allKeys)
            {
                var match = properties.Select(p => p.Name).Where(p => p == key).FirstOrDefault() ?? throw new ArgumentOutOfRangeException($"no match {key}");

                validKeys.Add(key);
            }

            return validKeys;
        }

        private string GetTableQueryPart()
        {
            var tableQuery = TableName;

            foreach (var condition in SpecialConditions)
                if (ValidKeys.Contains(condition.PropertyName))
                    tableQuery += condition.TableExtention;

            return tableQuery;
        }

        private string GetFiltersByNameQueryPart()
        {
            var filters = Parameters?.Filters?.ByName;

            if (filters == null)
                return string.Empty;

            List<string> expressions = new List<string>();

            foreach (var f in filters)
            {
                var fieldName = GetFieldName(f.Key);

                var expressionsInner = new List<string>();

                foreach (var v in f.Value)
                {
                    var expression = $"{fieldName} = N'{v}'";
                    expressionsInner.Add(expression);
                }

                expressions.Add($"({string.Join(" or ", expressionsInner)})");
            }

            return $" where {string.Join(" and ", expressions)}";
        }

        private string GetSortOrdersQueryPart()
        {
            var sort = Parameters?.Sort;

            if (sort == null)
                return string.Empty;

            List<string> sortOrders = new List<string>();

            foreach (var s in sort.Reverse())
            {
                var fieldName = GetFieldName(s.Key);

                var order = $"{fieldName} {GetSortDirection(s.Value)}";

                sortOrders.Add(order);
            }

            return $" order by {string.Join(",", sortOrders)}";
        }

        private string GetFieldName(string name)
        {
            return ConditionsProperties.Contains(name) ? SpecialConditions.Where(c => c.PropertyName == name).Select(c => c.TableField).FirstOrDefault() : $"{TableName}.{name}";
        }

        private string GetSortDirection(string direction)
        {
            switch (direction)
            {
                case "down":
                    return "desc";
                default:
                    return "asc";
            }
        }
    }
}
