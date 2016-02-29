using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DBConnect;
using DataFramework;
using System.Data;


public class ModelDataManager<M> where M : BaseModel
{
    private DBConnector Connector;

    public ModelDataManager(DBConnector connector)
    {
        Connector = connector;
        Connector.OpenConnection();
    }

    public List<M> GetAll()
    {
        var model = (M)Activator.CreateInstance(typeof(M));
        var sqlQuery = string.IsNullOrEmpty(model.SqlQuery_GetAll)
            ? Connector.CommonQueries.SelectAll(model.TableName)
            : model.SqlQuery_GetAll;
        var dataTable = Connector.OpenQuery(sqlQuery);
        var list = GetFromDataTable(dataTable);
        return list;
    }

    public M Get(Guid Id)
    {
        var model = (M)Activator.CreateInstance(typeof(M));
        var sqlQuery = string.IsNullOrEmpty(model.SqlQuery_Get)
            ? Connector.CommonQueries.SelectByID(model.TableName, model.PrimaryKeyFieldName)
            : model.SqlQuery_Get;
        Connector["@" + model.PrimaryKeyFieldName] = Id;
        var dataTable = Connector.OpenQuery(sqlQuery);
        var list = GetFromDataTable(dataTable);
        return list.FirstOrDefault();
    }

    public void Save(M model)
    {
        var dataFields = model.GetModelDataFields().Where(p => p != model.PrimaryKeyProperty);
        List<string> dataFieldNames = dataFields.Select(x => x.Name).ToList();
        string sqlQuery;
        if (model.IsNew)
        {
            sqlQuery = string.IsNullOrEmpty(model.SqlQuery_Insert)
                ? Connector.CommonQueries.Insert(model.TableName, dataFieldNames)
                : model.SqlQuery_Insert;
        }
        else
        {
            sqlQuery = string.IsNullOrEmpty(model.SqlQuery_Update)
                ? Connector.CommonQueries.Update(model.TableName, dataFieldNames, model.PrimaryKeyFieldName)
                : model.SqlQuery_Update;
            Connector["@" + model.PrimaryKeyFieldName] = model.PrimaryKeyValue;
        }
        foreach (var field in dataFields)
            Connector["@" + field.Name] = field.GetValue(model);
        Connector.ExecuteQuery(sqlQuery);
    }

    public void Delete(Guid Id)
    {
        var model = (M)Activator.CreateInstance(typeof(M));
        var sqlQuery = string.IsNullOrEmpty(model.SqlQuery_Delete)
            ? Connector.CommonQueries.DeleteByID(model.TableName, model.PrimaryKeyFieldName)
            : model.SqlQuery_Delete;
        Connector["@" + model.PrimaryKeyFieldName] = Id;
        Connector.ExecuteQuery(sqlQuery);
    }

    private List<M> GetFromDataTable(DataTable dataTable)
    {
        List<M> list = new List<M>();
        foreach (DataRow row in dataTable.Rows)
        {
            M model = GetFromDataRow(row);
            list.Add(model);
        }
        return list;
    }

    private M GetFromDataRow(DataRow dr)
    {
        M model = Activator.CreateInstance<M>();
        foreach (DataColumn column in dr.Table.Columns)
        {
            foreach (PropertyInfo property in typeof(M).GetProperties())
            {
                if (property.Name.ToLower() == column.ColumnName.ToLower() && property.GetSetMethod() != null)
                    property.SetValue(model, dr[column.ColumnName], null);
                else
                    continue;
            }
        }
        return model;
    }
}

