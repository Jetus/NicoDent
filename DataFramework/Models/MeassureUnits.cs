using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework
{
    [Model]
    public class MeassureUnits : BaseModel
    {
        [DataField(PrimaryKey = true)]
        public Guid UnitID { get; set; }

        [DataField]
        public string UnitName { get; set; }

        [DataField]
        public string UnitShortName { get; set; }
    }
}
