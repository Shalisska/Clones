using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Helpers
{
    public class ConditionsForFK
    {
        public ConditionsForFK(
            string propertyName,
            string tableExtention,
            string tableField)
        {
            PropertyName = propertyName;
            TableExtention = tableExtention;
            TableField = tableField;
        }

        public string PropertyName { get; set; }
        public string TableExtention { get; set; }
        public string TableField { get; set; }
    }
}
