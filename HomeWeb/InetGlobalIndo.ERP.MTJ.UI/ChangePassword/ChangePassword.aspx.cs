using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Authentication;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Home.ChangePassword
{
    public partial class ChangePassword : ChangePasswordBase
    {
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SavePassButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";

                this.ClearLabel();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SavePassButton_Click(object sender, ImageClickEventArgs e)
        {
            UserBL _userBL = new UserBL();

            bool _resultPass = false;

            this.ClearLabel();

            if (this.PasswordTextBox.Text != "" || this.PasswordCfmTextBox.Text != "")
            {
                _resultPass = _userBL.ChangePassword(HttpContext.Current.User.Identity.Name, this.OldPasswordTextBox.Text, this.PasswordTextBox.Text);
            }

            if (_resultPass == true)
            {
                //Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                this.WarningLabel.Text = "You succesfully edited the password";
            }
            else
            {
                this.WarningLabel.Text = "You Failed Change Password";
            }
        }
    }
}