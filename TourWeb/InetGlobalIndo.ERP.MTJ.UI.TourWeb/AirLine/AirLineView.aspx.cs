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
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;


namespace InetGlobalIndo.ERP.MTJ.UI.Tour.AirLine
{
    public partial class AirLineView : AirLineBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private AirLineBL _airLineBL = new AirLineBL();
        private SupplierBL _supBL = new SupplierBL();
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
            MsAirline _msAirline = this._airLineBL.GetSingleAirLine(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.AirLineCodeTextBox.Text = _msAirline.AirlineCode;
            this.AirLineNameTextBox.Text = _msAirline.AirlineName;
            this.SupplierTextBox.Text = this._supBL.GetSuppNameByCode(_msAirline.SuppCode);
            //this.SupplierLabel.Text = this._supBL.GetSuppNameByCode(_msAirline.SuppCode);
            this.CurrCodeTextBox.Text = _msAirline.CurrCode;
            this.Address1TextBox.Text = _msAirline.Address1;
            this.Address2TextBox.Text = _msAirline.Address2;
            this.CityTextBox.Text = _msAirline.City;
            this.PostCodeTextBox.Text = _msAirline.PostCode;
            this.TelephoneTextBox.Text = _msAirline.Telephone;
            this.FaxTextBox.Text = _msAirline.Fax;
            this.EmailTextBox.Text = _msAirline.Email;
            //this.FgActiveCheckBox.Checked = CustomerDataMapper.IsActive(Convert.ToChar(_msAirline.fgActive));
            this.RemarkTextBox.Text = _msAirline.Remark;
            this.LimitTextBox.Text = Convert.ToDecimal(_msAirline.AirlineLimit).ToString("#,##0.00");
            this.ContactPersonTextBox.Text = _msAirline.ContactPerson;
            this.ContactTitleTextBox.Text = _msAirline.ContactTitle;
            this.ContactPhoneTextBox.Text = _msAirline.ContactHP;
            this.ProductCodeTextBox.Text = this._productBL.GetProductNameByCode(_msAirline.ProductCode);
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