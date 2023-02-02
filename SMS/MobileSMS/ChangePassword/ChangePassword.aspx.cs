using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

public partial class ChangePassword : System.Web.UI.Page
{
    ManageUserBL _manageUserBL = new ManageUserBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["userID"] != null && HttpContext.Current.Session["Organization"] != null && HttpContext.Current.Session["FgWebAdmin"] != null)
        {
            if ((HttpContext.Current.Session["userID"].ToString() == "") || (HttpContext.Current.Session["Organization"].ToString() == "") || (HttpContext.Current.Session["FgWebAdmin"].ToString() == ""))
                Response.Redirect("../Login/Login.aspx");
        }
        else Response.Redirect("../Login/Login.aspx");
    }

    protected void ButtonChangePassword_Click(object sender, EventArgs e)
    {
        if (this.newPasswordTextBox.Text != "")
            if (this.newPasswordTextBox.Text == this.retypeNewPasswordTextBox.Text)
                if (_manageUserBL.ChangePassword(Session["Organization"].ToString(), Session["UserID"].ToString(), this.oldPasswordTextBox.Text, this.newPasswordTextBox.Text))
                    this.WarningLabel.Text = "Password has beed changed.";
                else
                    this.WarningLabel.Text = "Old Password was not correct.";
            else
                this.WarningLabel.Text = "Retyped Password was not match.";
        else
            this.WarningLabel.Text = "New Password must be filled.";
    }
}
