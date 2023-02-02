using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.LimitAuthorization
{
    public partial class LimitAuthorizationEdit : LimitAuthorizationBase
    {
        private LimitAuthorizationBL _limitAuthorBL = new LimitAuthorizationBL();
        private TransTypeBL _transTypeBL = new TransTypeBL();
        private RoleBL _roleBL = new RoleBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.LimitTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + this.LimitTextBox.ClientID + ");");
        }

        protected void ShowData()
        {
            Master_LimitAuthorization _msLimitAuthor = this._limitAuthorBL.GetSingleLimitAuthor(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._transTypeKey), ApplicationConfig.EncryptionKey));

            this.RoleTextBox.Text = _roleBL.GetRoleNameByCode(_msLimitAuthor.RoleID.ToString());
            this.TransTypeTextBox.Text = _transTypeBL.GetTransTypeNameByCode(_msLimitAuthor.TransTypeCode);
            this.LimitTextBox.Text = (_msLimitAuthor.Limit == 0) ? "0" : (_msLimitAuthor.Limit).ToString("#,###.##");
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            Master_LimitAuthorization _msLimitAuthor = this._limitAuthorBL.GetSingleLimitAuthor(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._transTypeKey), ApplicationConfig.EncryptionKey));

            _msLimitAuthor.Limit = Convert.ToDecimal(this.LimitTextBox.Text);

            _msLimitAuthor.EditBy = HttpContext.Current.User.Identity.Name;
            _msLimitAuthor.EditDate = DateTime.Now;

            bool _result = this._limitAuthorBL.EditLimitAuthor(_msLimitAuthor);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}