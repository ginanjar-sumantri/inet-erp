using System;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Authentication;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using System.Web.Security;
using System.Web;
using System.Security.Cryptography;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.User
{
    public partial class UserChangePassword : UserBase
    {
        private UserBL _userBL = new UserBL();
        
        private string _message = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SavePassButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                if (this._nvcExtractor.GetValue(this._message) != "")
                {
                    this.WarningLabel.Text = InetGlobalIndo.ERP.MTJ.Common.Encryption.Rijndael.Decrypt(this._nvcExtractor.GetValue(this._message), ApplicationConfig.EncryptionKey);
                }

                this.ShowData();
                this.ClearLabel();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            aspnet_User _aspuser = this._userBL.GetSingle(InetGlobalIndo.ERP.MTJ.Common.Encryption.Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.UserNameLabel.Text = _aspuser.UserName;

            this.PasswordCfmTextBox.Text = "";
            this.PasswordTextBox.Text = "";
        }

        protected void SavePassButton_Click(object sender, ImageClickEventArgs e)
        {
            bool _resultPass = false;
            if (this.PasswordTextBox.Text != "" && this.PasswordCfmTextBox.Text == this.PasswordTextBox.Text)
            {
                _resultPass = this._userBL.ChangePasswordWithoutOld(this.UserNameLabel.Text, this.PasswordTextBox.Text);
            }

            if (_resultPass == true)
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You succesfully edited the password";
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Change Password";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
            this.ClearLabel();
        }

        

    }
}