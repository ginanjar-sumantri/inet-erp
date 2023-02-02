using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SMSAdminMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["userAdmin"] != null )
        {
            if (HttpContext.Current.Session["userAdmin"].ToString() == "")
                Response.Redirect("../BackEndLogin/BackEndLogin.aspx");
        }
        else Response.Redirect("../BackEndLogin/BackEndLogin.aspx");

        //this.userIDLabel.Text = Session["userAdmin"].ToString();
    }

    protected void btnLogout_Click(object sender, ImageClickEventArgs e)
    {
        HttpContext.Current.Session["userAdmin"] = "";

        Response.Redirect("../BackEndLogin/BackEndLogin.aspx");
    }
}
