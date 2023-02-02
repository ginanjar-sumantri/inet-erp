using System;
using System.Web;
using System.Web.UI;
using VTSWeb.Database;
using VTSWeb.BusinessRule;
using VTSWeb.SystemConfig;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public partial class UserGroupAdd : UserGroupBase
    {
        private UserGroupBL _UserGroupBL = new UserGroupBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {
                Response.Redirect("..\\Login.aspx");
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = "../images/next.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";

                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.ClearLabel();

            this.UserGroupCodeTextBox.Text = "";
            this.UserGroupNameTextBox.Text = "";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            MsUserGroup _msUserGroup = new MsUserGroup();

            _msUserGroup.UserGroupCode = this.UserGroupCodeTextBox.Text;
            _msUserGroup.UserGroupName = this.UserGroupNameTextBox.Text;

            bool _result = this._UserGroupBL.Add(_msUserGroup);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.UserGroupCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}