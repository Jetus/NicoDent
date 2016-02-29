using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect
{
    public class MSSQLQueries
    {
        public string SelectAll(string tableName)
        {
            return string.Format("SELECT * FROM [{0}]", tableName);
        }

        public string SelectByID(string tableName, string IDFieldName)
        {
            return string.Format("SELECT * FROM [{0}] WHERE [{1}] = @{1}", tableName, IDFieldName);
        }

        public string Insert(string tableName, List<string> fieldsList)
        {
            var fields = string.Join(", ", fieldsList.Select(s => string.Format("[{0}]", s)));
            var parameters = string.Join(", ", fieldsList.Select(s => string.Format("@{0}", s)));
            return string.Format("INSERT INTO [{0}] ({1}) VALUES ({2})", tableName, fields, parameters);
        }

        public string Update(string tableName, List<string> fieldsList, string IDFieldName)
        {
            fieldsList = fieldsList.Select(s => string.Format("[{0}] = @{0}", s)).ToList();
            return string.Format("UPDATE [{0}] SET {1} WHERE [{2}] = @{2}", tableName, string.Join(", ", fieldsList), IDFieldName);
        }

        public string DeleteByID(string tableName, string IDFieldName)
        {
            return string.Format("DELETE FROM [{0}] WHERE [{1}] = @{1}", tableName, IDFieldName);
        }
    }
}
