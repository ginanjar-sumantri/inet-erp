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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["userID"] != null && HttpContext.Current.Session["Organization"] != null && HttpContext.Current.Session["FgWebAdmin"] != null)
        {
            if ((HttpContext.Current.Session["userID"].ToString() == "") || (HttpContext.Current.Session["Organization"].ToString() == "") || (HttpContext.Current.Session["FgWebAdmin"].ToString() == ""))
                Response.Redirect("../Login/Login.aspx");
        }
        else Response.Redirect("../Login/Login.aspx");
        if ((_loginBL.getPackageName(Session["Organization"].ToString(), Session["UserID"].ToString()) == "PERSONAL") && (Session["FgAdmin"].ToString() == "False"))
        {
            this.panelStatistic.Visible = this.paneLBlockPhoneNumber.Visible = this.panelGroup.Visible = this.panelContact.Visible = this.panelInbox.Visible = this.panelJunk.Visible = false;
        }
        if ((_loginBL.getPackageName(Session["Organization"].ToString(), Session["UserID"].ToString()) == "PERSONAL") && (Session["FgAdmin"].ToString() == "False"))
        {
            this.panelStatistic.Visible = this.paneLBlockPhoneNumber.Visible = this.panelGroup.Visible = this.panelContact.Visible = this.panelInbox.Visible = this.panelJunk.Visible = false;
        }
        if (Session["FgAdmin"].ToString() != "True")
        {
            this.panelMenuAdmin.Visible = false;
            this.panelCorporateFeature.Visible = (_loginBL.getPackageName(Session["Organization"].ToString(), Session["UserID"].ToString()) == "CORPORATE");
        }
        else { this.panelCorporateFeature.Visible = false; }

        this.browserBouncer.Text = "<script type='text/javascript'>if ('Mozilla' == navigator.appCodeName)window.location = 'http://" + _loginBL.getDomainName() + "';</script>";

        //this.userIDLabel.Text = Session["userID"].ToString();
        this.newInbox.Text = _loginBL.GetNewInbox(Session["Organization"].ToString(), Session["UserID"].ToString(), Session["FgAdmin"].ToString()).ToString();
        this.newOutbox.Text = _loginBL.GetNewOutbox(Session["Organization"].ToString(), Session["UserID"].ToString(), Session["FgAdmin"].ToString()).ToString();
    }
    protected void LogoutLinkButton_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Session["userID"] = "";
        HttpContext.Current.Session["Organization"] = "";
        HttpContext.Current.Session["FgWebAdmin"] = "";
        Response.Redirect("../Login/Login.aspx");
    }
}
