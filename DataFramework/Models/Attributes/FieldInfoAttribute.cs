using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldInfoAttribute : Attribute
    {
        public bool IsName { get; set; }
        public string Caption { get; set; }

        public FieldInfoAttribute()
        {
            IsName = false;
        }
    }
}
