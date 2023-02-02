using System;
using System.Web;
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
    public partial class LimitAuthorizationView : LimitAuthorizationBase
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

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();

                this.ShowData();
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        protected void ShowData()
        {
            Master_LimitAuthorization _msLimitAuthor = this._limitAuthorBL.GetSingleLimitAuthor(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._transTypeKey), ApplicationConfig.EncryptionKey));

            this.RoleTextBox.Text = _roleBL.GetRoleNameByCode(_msLimitAuthor.RoleID.ToString());
            this.TransTypeTextBox.Text = _transTypeBL.GetTransTypeNameByCode(_msLimitAuthor.TransTypeCode);
            this.LimitTextBox.Text = (_msLimitAuthor.Limit == 0) ? "0" : (_msLimitAuthor.Limit).ToString("#,###.##");
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._transTypeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._transTypeKey)));
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}