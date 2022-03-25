using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Net_Project6.Content
{
    public partial class AdminPanel : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Admin Panel/Login.aspx", true);
            }

            if(!Page.IsPostBack)
            {
                if(Session["DisplayName"] != null)
                {
                    lblUserName.Text = "HI" + Session["DisplayName"] + "!";
                }
                
            }
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Admin Panel/Login.aspx", true);
        }
    }
}