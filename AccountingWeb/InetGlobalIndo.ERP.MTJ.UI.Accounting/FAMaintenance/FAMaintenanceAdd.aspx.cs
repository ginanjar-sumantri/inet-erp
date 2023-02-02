using System;
using System.Web;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAMaintanance
{
    public partial class FAMaintenanceAdd : FAMaintananceBase
    {
        private FixedAssetsBL _fixedAssets = new FixedAssetsBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.SetUpButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/set_detail.jpg";

                this.SetAttribute();
                this.ClearLabel();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.FgAddValueCheckBox.Attributes.Add("OnClick", " EnableDisableButtonAuthor('" + this.SetUpButton.ClientID + "');");
        }

        protected void ClearData()
        {
            this.FixAssetsCodeTextBox.Text = "";
            this.FixAssetsNameTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            MsFAMaintenance _fixed = new MsFAMaintenance();

            _fixed.FAMaintenanceCode = this.FixAssetsCodeTextBox.Text;
            _fixed.FAMaintenanceName = this.FixAssetsNameTextBox.Text;
            _fixed.FgAddValue = FixedAssetsDataMapper.IsAllowAddValue(this.FgAddValueCheckBox.Checked);
            _fixed.UserId = HttpContext.Current.User.Identity.Name;
            _fixed.UserDate = DateTime.Now;

            bool _result = this._fixedAssets.Add(_fixed);

            if (_result == true)
            {
                Response.Redirect(_homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ClearData();
        }

        protected void SetUpButton_Click(object sender, EventArgs e)
        {
            MsFAMaintenance _fixed = new MsFAMaintenance();

            _fixed.FAMaintenanceCode = this.FixAssetsCodeTextBox.Text;
            _fixed.FAMaintenanceName = this.FixAssetsNameTextBox.Text;
            _fixed.FgAddValue = FixedAssetsDataMapper.IsAllowAddValue(this.FgAddValueCheckBox.Checked);
            _fixed.UserId = HttpContext.Current.User.Identity.Name;
            _fixed.UserDate = DateTime.Now;

            bool _result = this._fixedAssets.Add(_fixed);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.FixAssetsCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }
    }
}