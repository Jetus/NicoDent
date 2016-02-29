using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DataFramework;

    public partial class Login : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e) {
            if (Membership.ValidateUser(tbUserName.Text, tbPassword.Text))
            {
                //var currentUserId = (Guid)Membership.GetUser().ProviderUserKey;
                //var user = CreateModelManager<UserProfile>().GetAll().FirstOrDefault(x => x.UserId == currentUserId);
                //Session["_CurrentUser"] = user;
                if(string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                {
                    FormsAuthentication.SetAuthCookie(tbUserName.Text, false);
                    Response.Redirect("~/");
                }
                else
                    FormsAuthentication.RedirectFromLoginPage(tbUserName.Text, false);
            }
            else {
                tbUserName.ErrorText = "Не вірне ім'я користувача чи пароль";
                tbUserName.IsValid = false;
            }
        }
    }