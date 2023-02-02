using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.ChangePassword
{
    public partial class ChangePassword : SMSWebBase
    {
        ManageUserBL _manageUserBL = new ManageUserBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CekSession();
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
}