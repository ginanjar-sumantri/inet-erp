using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.BackEndSMSPortal.BackEndLogin
{
    public partial class BackEndLogin : System.Web.UI.Page
    {
        BackEndBL _backEndBL = new BackEndBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.ClearLabel();
            }
        }

        private void ClearLabel()
        {
            this.WarrningLabel.Text = "";
            this.UserAdminTextBox.Text = "";
            this.PasswordAdminTextBox.Text = "";
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (this.UserAdminTextBox.Text != "" && this.PasswordAdminTextBox.Text != "")
            {
                bool _result = _backEndBL.CekLoginAdmin(this.UserAdminTextBox.Text, Rijndael.Encrypt(this.PasswordAdminTextBox.Text, ApplicationConfig.EncryptionKey));

                if (_result)
                {
                    HttpContext.Current.Session["userAdmin"] = this.UserAdminTextBox.Text;
                    Response.Redirect("../RegisterUser/RegisterUser.aspx");
                }
                else
                {
                    this.WarrningLabel.Text = "Your User Admin & Password is not valid";
                }
            }
        }
    }
}