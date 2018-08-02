using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clones.Models
{
    public class BaseViewModel<T>
        where T : class
    {
        public IEnumerable<T> ModelList { get; set; }

        public IDictionary<string, object> Item { get; set; }

        public void Add(string key, object value)
        {
            if (Item == null)
                Item = new Dictionary<string, object>();

            Item.Add(key, value);
        }
    }
}