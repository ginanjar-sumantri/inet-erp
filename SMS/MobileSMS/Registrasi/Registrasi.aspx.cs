using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using SMSLibrary;

namespace SMS.SMSWeb.Registrasi
{
    public partial class Registrasi : System.Web.UI.Page
    {
        LoginBL _loginBL = new LoginBL();
        RegistrationBL _registrationBL = new RegistrationBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.browserBouncer.Text = "<script type='text/javascript'>if ('Mozilla' == navigator.appCodeName)window.location = 'http://" + _loginBL.getDomainName() + "';</script>";
            if (!this.Page.IsPostBack == true)
            {
                SetVerificationText();
            }
        }

        private void SetVerificationText()
        {
            Random ran = new Random();
            int no = ran.Next();
            Session["Captcha"] = no.ToString();
        }

        protected void CAPTCHAValidate(object source, ServerValidateEventArgs args)
        {
            if (Session["Captcha"] != null)
            {
                if (txtVerify.Text != Session["Captcha"].ToString())
                {
                    SetVerificationText();
                    args.IsValid = false;
                    return;
                }
            }
            else
            {
                SetVerificationText();
                args.IsValid = false;
                return;
            }
        }

        protected void SaveImageButton_Click(object sender, EventArgs e)
        {
            if (Session["captcha"] != null && txtVerify.Text.ToLower() == Session["captcha"].ToString())
            {

                if (this.CorporateNameTextBox.Text != "" && this.UserIDTextBox.Text != "" && this.PasswordTextBox.Text != "" && this.ConfirmPasswordTextBox.Text != "")
                {
                    MsUser _msUser = new MsUser();

                    int _count = _registrationBL.GetOrganizationID(this.CorporateNameTextBox.Text);
                    _msUser.OrganizationID = _count;
                    _msUser.UserID = this.UserIDTextBox.Text;
                    _msUser.password = Rijndael.Encrypt(this.PasswordTextBox.Text, ApplicationConfig.EncryptionKey);
                    _msUser.fgAdmin = false;
                    _msUser.fgWebAdmin = false;
                    _msUser.RegistrationDate = DateTime.Now;
                    _msUser.ExpiredDate = DateTime.Now.AddYears(1);
                    _msUser.PackageName = "PERSONAL";
                    _msUser.SMSLimit = new PackageBL().GetSMSPerDayByCode("PERSONAL");
                    _msUser.LastLimitReset = DateTime.Now;
                    _msUser.Email = this.EmailTextBox.Text;
                    _msUser.fgActive = false;

                    bool _submit1 = _registrationBL.AddUser(this.CorporateNameTextBox.Text, _msUser, "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + "/");

                    if (_submit1 == true)
                    {
                        Response.Redirect("../Login/Login.aspx");
                    }
                    //}
                    //}
                    //else
                    //{
                    //    this.WarningLabel.Text = "Corporation Name already exist";
                    //}
                }
                else
                {
                    this.WarningLabel.Text = "Please, insert All The Text Box";
                }
            }
            else
            {
                this.WarningLabel.Text = "Capcha text did not match.";
            }
        }
    }
}
