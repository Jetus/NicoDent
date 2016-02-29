using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework
{
    [Model]
    public class ProductsCategories : BaseModel
    {
        [DataField(PrimaryKey = true)]
        public Guid CategoryID { get; set; }

        [DataField]
        public string CategoryName { get; set; }
    }
}
