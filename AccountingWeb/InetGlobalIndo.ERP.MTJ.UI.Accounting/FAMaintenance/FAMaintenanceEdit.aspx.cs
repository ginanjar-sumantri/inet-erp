using System;
using System.Web;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAMaintanance
{
    public partial class FAMaintenanceEdit : FAMaintananceBase
    {
        private FixedAssetsBL _fixed = new FixedAssetsBL();
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
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.FgAddValueCheckBox.Attributes.Add("OnClick", " EnableDisableButtonAuthor('" + this.SaveAndViewDetailButton.ClientID + "');");
        }

        protected void ShowData()
        {
            MsFAMaintenance _msFaMaintenance = this._fixed.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.FixAssetsCodeTextBox.Text = _msFaMaintenance.FAMaintenanceCode;
            this.FixAssetsNameTextBox.Text = _msFaMaintenance.FAMaintenanceName;
            this.FgAddValueCheckBox.Checked = FixedAssetsDataMapper.IsFGAddValue(_msFaMaintenance.FgAddValue);
        }

        protected bool Save()
        {
            MsFAMaintenance _fixed = this._fixed.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _fixed.FAMaintenanceCode = this.FixAssetsCodeTextBox.Text;
            _fixed.FAMaintenanceName = this.FixAssetsNameTextBox.Text;
            _fixed.FgAddValue = FixedAssetsDataMapper.IsAllowAddValue(this.FgAddValueCheckBox.Checked);
            _fixed.UserId = HttpContext.Current.User.Identity.Name;
            _fixed.UserDate = DateTime.Now;

            bool _result = this._fixed.Edit(_fixed);

            return _result;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            bool _result = Save();

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
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

        protected void ViewDetailButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.FixAssetsCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, EventArgs e)
        {
            bool _result = Save();

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.FixAssetsCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }
    }
}