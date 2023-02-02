using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.Login
{
    public partial class Login : System.Web.UI.Page
    {
        LoginBL _loginBL = new LoginBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.browserBouncer.Text = "<script type='text/javascript'>if ('Mozilla' == navigator.appCodeName)window.location = 'http://" + _loginBL.getDomainName() + "';</script>";
            if (!Page.IsPostBack)
            {
                this.SetVerificationText();
            }
        }

        private void SetVerificationText()
        {
            Random ran = new Random();
            int no = ran.Next(0, 9999);
            Session["Captcha"] = no.ToString("0000");
        }

        protected void CAPTCHAValidate(object source, ServerValidateEventArgs args)
        {
            if (Session["Captcha"] != null)
            {
                if (this.txtVerify.Text != Session["Captcha"].ToString())
                {
                    this.SetVerificationText();
                    args.IsValid = false;
                    return;
                }
            }
            else
            {
                this.SetVerificationText();
                args.IsValid = false;
                return;
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (Session["captcha"] != null && this.txtVerify.Text.ToLower() == Session["captcha"].ToString())
            {
                if (this.CorporateNameTextBox.Text != "" && this.UserIDTextBox.Text != "" && this.PasswordTextBox.Text != "")
                {
                    Double _corp = this._loginBL.GetSingleCorpID(this.CorporateNameTextBox.Text);
                    if (_corp != 0)
                    {
                        String _password = Rijndael.Encrypt(this.PasswordTextBox.Text, ApplicationConfig.EncryptionKey);
                        String _ErrorMsg = "";
                        bool _result = this._loginBL.toLogin(Convert.ToInt32(_corp), this.UserIDTextBox.Text, _password, DateTime.Now, ref _ErrorMsg);
                        //bool _result = this._loginBL.toLogin(Convert.ToInt32(_corp), this.UserIDTextBox.Text, _password, DateTime.Now);

                        if (_result == true)
                        {
                            bool _result1 = this._loginBL.GetFgWebAdmin(_corp, this.UserIDTextBox.Text, _password);
                            HttpContext.Current.Session["userID"] = this.UserIDTextBox.Text;
                            HttpContext.Current.Session["Organization"] = _corp;
                            HttpContext.Current.Session["FgWebAdmin"] = _result1;
                            Response.Redirect("../Message/Compose.aspx");
                        }
                        else
                        {
                            this.WarningLabel.Text = "User ID not Register or Expired";
                        }
                    }
                    else
                    {
                        this.WarningLabel.Text = "Corporation Name not Register";
                    }
                }
                else
                {
                    this.WarningLabel.Text = "Please, insert Corporate Name, User ID, Password";
                }
            }
            else
            {
                this.WarningLabel.Text = "Capcha text did not match.";
            }
        }
    }
}
