using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;


namespace InetGlobalIndo.ERP.MTJ.UI.Tour.Hotel
{
    public partial class HotelView : HotelBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private HotelBL _HotelBL = new HotelBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CityBL _cityBL = new CityBL();
        private ProductBL _productBL = new ProductBL();

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

                this.ShowData();
                this.SetButtonPermission();
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

        public void ShowData()
        {
            POSMsHotel _msHotel = this._HotelBL.GetSinglePOSMsHotel(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.HotelCodeTextBox.Text = _msHotel.HotelCode;
            this.HotelNameTextBox.Text = _msHotel.HotelName;
            this.SupplierTextBox.Text = _supplierBL.GetSuppNameByCode(_msHotel.SuppCode);
            if (_msHotel.CurrCode == "")
            {
                this.CurrCodeTextBox.Text = "";
                
            }
            else
            {
                this.CurrCodeTextBox.Text = _currencyBL.GetCurrencyName(_msHotel.CurrCode);
            }

            this.Address1TextBox.Text = _msHotel.Address1;
            this.Address2TextBox.Text = _msHotel.Address2;
            this.CityTextBox.Text = _cityBL.GetCityNameByCode(_msHotel.CityCode);
            this.PostCodeTextBox.Text = _msHotel.PostCode;
            this.TelephoneTextBox.Text = _msHotel.Telephone;
            this.FaxTextBox.Text = _msHotel.Fax;
            this.EmailTextBox.Text = _msHotel.Email;
            this.FgActiveCheckBox.Checked = CustomerDataMapper.IsActive(Convert.ToChar(_msHotel.fgActive));
            this.RemarkTextBox.Text = _msHotel.Remark;
            this.ContactPersonTextBox.Text = _msHotel.ContactPerson;
            this.ContactTitleTextBox.Text = _msHotel.ContactTitle;
            this.ContactPhoneTextBox.Text = _msHotel.ContactHP;
            this.ProductCodeTextBox.Text = this._productBL.GetProductNameByCode(_msHotel.ProductCode);

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