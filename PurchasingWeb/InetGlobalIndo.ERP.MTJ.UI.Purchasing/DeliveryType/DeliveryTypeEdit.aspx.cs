using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.DeliveryType
{
    public partial class DeliveryEdit : DeliveryTypeBase
    {
        private DeliveryBL _deliveryBL = new DeliveryBL();
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
                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.ZipCodeTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.PhoneTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.FaxTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
        }

        public void ShowData()
        {
            MsDelivery _msDelivery = this._deliveryBL.GetSingleDelivery(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.DeliveryCodeTextBox.Text = _msDelivery.DeliveryCode;
            this.DeliveryNameTextBox.Text = _msDelivery.DeliveryName;
            this.Address1TextBox.Text = _msDelivery.Address1;
            this.Address2TextBox.Text = _msDelivery.Address2;
            this.ZipCodeTextBox.Text = _msDelivery.ZipCode;
            this.PhoneTextBox.Text = _msDelivery.Phone;
            this.FaxTextBox.Text = _msDelivery.Fax;
            this.ContactNameTextBox.Text = _msDelivery.ContactName;
            this.ContactTitleTextBox.Text = _msDelivery.ContactTitle;
            this.ContactHPTextBox.Text = _msDelivery.ContactHP;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsDelivery _msDelivery = this._deliveryBL.GetSingleDelivery(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msDelivery.DeliveryName = this.DeliveryNameTextBox.Text;
            _msDelivery.Address1 = this.Address1TextBox.Text;
            _msDelivery.Address2 = this.Address2TextBox.Text;
            _msDelivery.ZipCode = this.ZipCodeTextBox.Text;
            _msDelivery.Phone = this.PhoneTextBox.Text;
            _msDelivery.Fax = this.FaxTextBox.Text;
            _msDelivery.ContactName = this.ContactNameTextBox.Text;
            _msDelivery.ContactTitle = this.ContactTitleTextBox.Text;
            _msDelivery.ContactHP = this.ContactHPTextBox.Text;
            _msDelivery.UserId = HttpContext.Current.User.Identity.Name;
            _msDelivery.UserDate = DateTime.Now;

            bool _result = this._deliveryBL.EditDelivery(_msDelivery);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}