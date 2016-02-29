using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.Serialization;

namespace DataFramework
{
    public class BaseModel
    {
        private string _TableName = string.Empty;        
        
        public string TableName
        {
            get
            {
                if (!string.IsNullOrEmpty(_TableName))
                    return _TableName;
                var modelAttribute = GetType().GetCustomAttribute(typeof(ModelAttribute)) as ModelAttribute;
                if (modelAttribute != null)
                    _TableName = modelAttribute.TableName;
                if (string.IsNullOrEmpty(_TableName))
                    _TableName = GetType().Name;
                return _TableName;
            }
            set
            {
                _TableName = value;
            }
        }

        #region PrimaryKey 

        private PropertyInfo _PrimaryKeyProperty = null;

        public PropertyInfo PrimaryKeyProperty
        {
            get
            {
                if (_PrimaryKeyProperty != null)
                    return _PrimaryKeyProperty;
                var properties = GetModelDataFields();
                foreach (PropertyInfo property in properties)
                {
                    var dataFieldAttribute = property.GetCustomAttribute(typeof(DataFieldAttribute)) as DataFieldAttribute;
                    if (dataFieldAttribute.PrimaryKey)
                    {
                        _PrimaryKeyProperty = property;
                        break;
                    }
                }
                return _PrimaryKeyProperty;
            }
        }

        public Guid PrimaryKeyValue
        {
            get
            {
                if (PrimaryKeyProperty == null)
                    return Guid.Empty;
                return (Guid)PrimaryKeyProperty.GetValue(this);
            }            
        }

        public string PrimaryKeyFieldName
        {
            get
            {
                if (PrimaryKeyProperty == null)
                    return null;
                return PrimaryKeyProperty.Name;
            }
        }

        public bool IsNew
        {
            get
            {
                return PrimaryKeyValue == Guid.Empty;
            }
        }

        #endregion

        private List<PropertyInfo> _ModelDataFields = null;
        public List<PropertyInfo> GetModelDataFields()
        {
            if (_ModelDataFields != null)
                return _ModelDataFields;
            _ModelDataFields = new List<PropertyInfo>();
            var properties = GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var dataFieldAttribute = property.GetCustomAttribute(typeof(DataFieldAttribute)) as DataFieldAttribute;
                if (dataFieldAttribute != null)
                    _ModelDataFields.Add(property);
            }
            return _ModelDataFields;
        }

        #region SQL Queries

        public virtual string SqlQuery_GetAll
        {
            get
            {
                return string.Empty;
            }            
        }

        public virtual string SqlQuery_Get
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual string SqlQuery_Insert
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual string SqlQuery_Update
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual string SqlQuery_Delete
        {
            get
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
