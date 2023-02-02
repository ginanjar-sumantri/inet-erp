using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

public partial class SMSMasterPage : System.Web.UI.MasterPage
{
    protected LoginBL _loginBL = new LoginBL();
    protected void Page_Load(object sender, EventArgs e){
        if (HttpContext.Current.Session["UserID"] != null && HttpContext.Current.Session["Organization"] != null && HttpContext.Current.Session["FgWebAdmin"] != null)
        {
            if ((HttpContext.Current.Session["UserID"].ToString() == "") || (HttpContext.Current.Session["Organization"].ToString() == "") || (HttpContext.Current.Session["FgWebAdmin"].ToString() == ""))
                Response.Redirect("../Login/Login.aspx");
        }
        else Response.Redirect("../Login/Login.aspx");
        if ( (_loginBL.getPackageName (Session["Organization"].ToString(), Session["UserID"].ToString()) == "PERSONAL" ) && ( Session["FgWebAdmin"].ToString() == "False" ) ) {
            this.panelStatistic.Visible = this.paneLBlockPhoneNumber.Visible = this.panelGroup.Visible = this.panelContact.Visible = this.panelInbox.Visible = this.panelJunk.Visible = false;
        }
        if (Session["FgWebAdmin"].ToString() != "True")
        {
            this.panelMenuAdmin.Visible = false;
            this.CorporateFeaturePanel.Visible = (_loginBL.getPackageName(Session["Organization"].ToString(), Session["UserID"].ToString()) == "CORPORATE");
        }
        else { this.CorporateFeaturePanel.Visible = false; }
        this.userIDLabel.Text = Session["UserID"].ToString();
        this.newInbox.Text = _loginBL.GetNewInbox(Session["Organization"].ToString(),Session["UserID"].ToString(),Session["FgWebAdmin"].ToString()).ToString();
        this.newOutbox.Text = _loginBL.GetNewOutbox(Session["Organization"].ToString(), Session["UserID"].ToString(), Session["FgWebAdmin"].ToString()).ToString();
    }
    protected void btnLogout_Click(object sender, ImageClickEventArgs e)
    {
        HttpContext.Current.Session["UserID"] = "";
        HttpContext.Current.Session["Organization"] = "";
        HttpContext.Current.Session["FgWebAdmin"] = "";
        Response.Redirect("../Login/Login.aspx");
    }
}
