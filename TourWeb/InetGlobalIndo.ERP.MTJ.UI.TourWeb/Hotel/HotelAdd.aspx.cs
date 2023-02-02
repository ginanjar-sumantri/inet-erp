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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;


namespace InetGlobalIndo.ERP.MTJ.UI.Tour.Hotel
{
    public partial class HotelAdd : HotelBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private HotelBL _HotelBL = new HotelBL();
        private CityBL _cityBL = new CityBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private ProductBL _productBL = new ProductBL();

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

            this.btnSearchSupplier.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findSupplier&configCode=supplier','_popSearch','width=400,height=550,toolbar=0,location=0,status=0,scrollbars=1')";

            String spawnJS2 = "<script language='JavaScript'>\n";

            ////////////////////DECLARE FUNCTION FOR CATCHING SUPPLIER SEARCH
            spawnJS2 += "function findSupplier(x) {\n";
            spawnJS2 += "dataArray = x.split ('|') ;\n";
            spawnJS2 += "document.getElementById('" + this.SupplierTextBox.ClientID + "').value = dataArray[0];\n";
            spawnJS2 += "document.getElementById('" + this.SupplierLabel.ClientID + "').innerHTML = dataArray[1];\n";
            spawnJS2 += "document.forms[0].submit();\n";
            spawnJS2 += "}\n";

            spawnJS2 += "</script>\n";
            this.javascriptReceiver2.Text = spawnJS2;

            this.btnSearchProduct.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findProduct&configCode=product','_popSearch','width=1200,height=1000,toolbar=0,location=0,status=0,scrollbars=1')";

            String spawnJS = "<script language='JavaScript'>\n";

            spawnJS += "function findProduct(x) {\n";
            spawnJS += "dataArray = x.split ('|') ;\n";
            spawnJS += "document.getElementById('" + this.ProductCodeHiddenField.ClientID + "').value = dataArray[0];\n";
            spawnJS += "document.getElementById('" + this.ProductCodeTextBox.ClientID + "').value = dataArray[1];\n";
            spawnJS += "document.forms[0].submit();\n";
            spawnJS += "}\n";

            spawnJS += "</script>\n";
            this.javascriptReceiver.Text = spawnJS;


            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ShowCityDropdownlist();
                this.SetAttribute();
                this.ClearLabel();
                this.ClearData();
                this.ShowCurrencyDDL();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.TelephoneTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.TelephoneTextBox.ClientID + "," + this.TelephoneTextBox.ClientID + ",500" + ");");
            this.FaxTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.FaxTextBox.ClientID + "," + this.FaxTextBox.ClientID + ",500" + ");");
            this.PostCodeTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.PostCodeTextBox.ClientID + "," + this.PostCodeTextBox.ClientID + ",500" + ");");
            this.ContactPhoneTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.ContactPhoneTextBox.ClientID + "," + this.ContactPhoneTextBox.ClientID + ",500" + ");");
        }

        public void ClearData()
        {
            this.HotelCodeTextBox.Text = "";
            this.HotelNameTextBox.Text = "";
            this.Address1TextBox.Text = "";
            this.Address2TextBox.Text = "";
            this.TelephoneTextBox.Text = "";
            this.FaxTextBox.Text = "";
            this.EmailTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            this.PostCodeTextBox.Text = "";
            this.ContactPersonTextBox.Text = "";
            this.ContactTitleTextBox.Text = "";
            this.ContactPhoneTextBox.Text = "";
            this.SupplierTextBox.Text = "";
            this.ProductCodeTextBox.Text = "";
        }

        private void ShowCurrencyDDL()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListCurrForDDL();
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataTextField = "CurrName";
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCityDropdownlist()
        {
            this.CityDDL.Items.Clear();
            this.CityDDL.DataSource = this._cityBL.GetListCityForDDL();
            this.CityDDL.DataValueField = "CityCode";
            this.CityDDL.DataTextField = "CityName";
            this.CityDDL.DataBind();
            this.CityDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SupplierTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.SupplierTextBox.Text != "")
            {
                this.SupplierLabel.Text = _supplierBL.GetSuppNameByCode(this.SupplierTextBox.Text);
            }
            else
            {
                this.SupplierLabel.Text = " ";
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsHotel _msHotel = new POSMsHotel();

            _msHotel.HotelCode = this.HotelCodeTextBox.Text;
            _msHotel.HotelName = this.HotelNameTextBox.Text;
            _msHotel.SuppCode = this.SupplierTextBox.Text;
            _msHotel.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _msHotel.Address1 = this.Address1TextBox.Text;
            _msHotel.Address2 = this.Address2TextBox.Text;
            _msHotel.CityCode = this.CityDDL.SelectedValue;
            _msHotel.PostCode = this.PostCodeTextBox.Text;
            _msHotel.Telephone = this.TelephoneTextBox.Text;
            _msHotel.Fax = this.FaxTextBox.Text;
            _msHotel.Email = this.EmailTextBox.Text;
            _msHotel.fgActive = CustomerDataMapper.IsActive(this.FgActiveCheckBox.Checked);
            _msHotel.Remark = this.RemarkTextBox.Text;
            _msHotel.ContactPerson = this.ContactPhoneTextBox.Text;
            _msHotel.ContactTitle = this.ContactTitleTextBox.Text;
            _msHotel.ContactHP = this.ContactPhoneTextBox.Text;
            _msHotel.InsertBy = HttpContext.Current.User.Identity.Name;
            _msHotel.InsertDate = DateTime.Now;
            _msHotel.EditBy = HttpContext.Current.User.Identity.Name;
            _msHotel.EditDate = DateTime.Now;
            _msHotel.ProductCode = this.ProductCodeHiddenField.Value;

            bool _result = this._HotelBL.AddPOSMsHotel(_msHotel);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
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
            this.ClearLabel();
            this.ClearData();
        }
    }
}