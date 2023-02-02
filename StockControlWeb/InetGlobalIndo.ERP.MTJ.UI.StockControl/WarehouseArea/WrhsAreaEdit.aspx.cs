using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.WarehouseArea
{
    public partial class WrhsAreaEdit : WarehouseAreaBase
    {
        private WarehouseBL _wrhsArea = new WarehouseBL();
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
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetAttribute();
                this.ShowData();
            }
        }

        private void SetAttribute()
        {
            this.ZipCodeTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.PhoneTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.FaxTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsWrhsArea _msWrhsArea = this._wrhsArea.GetSingleWrhsArea(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msWrhsArea.WrhsAreaName = this.WrhsAreaNameTextBox.Text;
            _msWrhsArea.Address1 = this.Address1TextBox.Text;
            _msWrhsArea.Address2 = this.Address2TextBox.Text;
            _msWrhsArea.ContactEmail = this.ContactEmailTextBox.Text;
            _msWrhsArea.ContactPerson = this.ContactPersonTextBox.Text;
            _msWrhsArea.ContactPhone = this.ContactPhoneTextBox.Text;
            _msWrhsArea.ContactTitle = this.ContactTitleTextBox.Text;
            _msWrhsArea.Fax = this.FaxTextBox.Text;
            _msWrhsArea.Phone = this.PhoneTextBox.Text;
            _msWrhsArea.ZipCode = this.ZipCodeTextBox.Text;
            _msWrhsArea.UserId = HttpContext.Current.User.Identity.Name;
            _msWrhsArea.UserDate = DateTime.Now;

            bool _result = this._wrhsArea.EditWrhsArea(_msWrhsArea);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}