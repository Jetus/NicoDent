using DataFramework;
using DBConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
	public DBConnector Connector
    {
        get
        {
            var connector = Session["_Connector"] as DBConnector;
            if (connector == null)
            {
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                connector = new DBConnector(connectionString);
                Session["_Connector"] = connector;
            }
            return connector;
        }
    }

    public ModelDataManager<M> CreateModelManager<M>() where M : BaseModel
    {
        return new ModelDataManager<M>(Connector);
    }
}