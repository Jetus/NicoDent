using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DataFramework;
using System.Web.Security;
using DBConnect;

    public partial class RootMaster : System.Web.UI.MasterPage
    {
        private UserProfile CurrentUser
        {
            get
            {
                return Session["_CurrentUser"] as UserProfile;
            }
            set
            {
                Session["_CurrentUser"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxLabel2.Text = string.Format(Server.HtmlDecode("{0} &copy; {1}"), DateTime.Now.Year, "Jetus-Soft");
            CheckCurrentUser();
            pnlLeftPanel.Visible = CurrentUser != null;
            LoginName loginName = HeadLoginView.FindControl("HeadLoginName") as LoginName; // Page.Master.FindControl("HeadLoginName")
            if (loginName != null && pnlLeftPanel.Visible && Session != null)
            {
                var user = Session["_CurrentUser"] as UserProfile;
                if (user != null)
                    loginName.FormatString = user.FullName;
            }
        }

        private void CheckCurrentUser()
        {
            if (HttpContext.Current.User == null || HttpContext.Current.User.Identity == null || !HttpContext.Current.User.Identity.IsAuthenticated)
                CurrentUser = null;
            if (CurrentUser == null)
            {
                if (HttpContext.Current.User != null && HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                    var connector = new DBConnector(connectionString);
                    var currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                    CurrentUser = new ModelDataManager<UserProfile>(connector).GetAll().FirstOrDefault(x => x.UserId == currentUserId);
                }
            }
        }
    }