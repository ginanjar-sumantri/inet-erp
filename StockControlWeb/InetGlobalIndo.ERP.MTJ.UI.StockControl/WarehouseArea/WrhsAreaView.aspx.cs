using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.WarehouseArea
{
    public partial class WrhsAreaView : WarehouseAreaBase
    {
        private WarehouseBL _wrhsArea = new WarehouseBL();
        private AccountBL _acc = new AccountBL();
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
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowData();
                this.SetButtonPermission();
            }
        }

        private void SetButtonPermission()
        {
            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }

        }

        private void ShowData()
        {
            MsWrhsArea _msWrhsArea = this._wrhsArea.GetSingleWrhsArea(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.WrhsAreaCodeTextBox.Text = _msWrhsArea.WrhsAreaCode;
            this.WrhsAreaNameTextBox.Text = _msWrhsArea.WrhsAreaName;
            this.Address1TextBox.Text = _msWrhsArea.Address1;
            this.Address2TextBox.Text = _msWrhsArea.Address2;
            this.ContactEmailTextBox.Text = _msWrhsArea.ContactEmail;
            this.ContactPersonTextBox.Text = _msWrhsArea.ContactPerson;
            this.ContactPhoneTextBox.Text = _msWrhsArea.ContactPhone;
            this.ContactTitleTextBox.Text = _msWrhsArea.ContactTitle;
            this.FaxTextBox.Text = _msWrhsArea.Fax;
            this.PhoneTextBox.Text = _msWrhsArea.Phone;
            this.ZipCodeTextBox.Text = _msWrhsArea.ZipCode;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}