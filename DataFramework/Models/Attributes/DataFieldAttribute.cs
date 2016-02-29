using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataFieldAttribute : Attribute
    {
        public bool PrimaryKey { get; set; }
        public bool DeletedMark { get; set; }

        public DataFieldAttribute()
        {
            PrimaryKey = false;
            DeletedMark = false;
        }
    }
}
