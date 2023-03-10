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
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Tour.AirLine
{
    public partial class AirLineAdd : AirLineBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private AirLineBL _airLineBL = new AirLineBL();
        private CityBL _cityBL = new CityBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private SupplierBL _suppBL = new SupplierBL();

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
                this.ShowCurr();
                this.ClearLabel();
                this.ClearData();
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
            this.LimitTextBox.Attributes.Add("OnKeyup", "return formatangka(" + this.LimitTextBox.ClientID + "," + this.LimitTextBox.ClientID + ",500" + ");");
        }

        public void ClearData()
        {
            this.AirLineCodeTextBox.Text = "";
            this.AirLineNameTextBox.Text = "";
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
            this.SupplierLabel.Text = "";
        }

        public void ShowCurr()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataTextField = "CurrCode";
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.SelectedValue = _currencyBL.GetCurrDefault();
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsAirline _msAirline = new MsAirline();

            _msAirline.AirlineCode = this.AirLineCodeTextBox.Text;
            _msAirline.AirlineName = this.AirLineNameTextBox.Text;
            _msAirline.SuppCode = this.SupplierTextBox.Text;
            _msAirline.CurrCode = this.CurrCodeDropDownList.SelectedValue;
            _msAirline.Address1 = this.Address1TextBox.Text;
            _msAirline.Address2 = this.Address2TextBox.Text;
            _msAirline.City = this.CityDDL.SelectedValue;
            _msAirline.PostCode = this.PostCodeTextBox.Text;
            _msAirline.Telephone = this.TelephoneTextBox.Text;
            _msAirline.Fax = this.FaxTextBox.Text;
            _msAirline.Email = this.EmailTextBox.Text;
            _msAirline.fgActive = CustomerDataMapper.IsActive(this.FgActiveCheckBox.Checked);
            _msAirline.Remark = this.RemarkTextBox.Text;
            _msAirline.AirlineLimit = this.LimitTextBox.Text == "" ? 0 : Convert.ToDecimal(this.LimitTextBox.Text);
            _msAirline.ContactPerson = this.ContactPersonTextBox.Text;
            _msAirline.ContactTitle = this.ContactTitleTextBox.Text;
            _msAirline.ContactHP = this.ContactPhoneTextBox.Text;
            _msAirline.InsertBy = HttpContext.Current.User.Identity.Name;
            _msAirline.InsertDate = DateTime.Now;
            _msAirline.EditBy = HttpContext.Current.User.Identity.Name;
            _msAirline.EditDate = DateTime.Now;
            _msAirline.ProductCode = this.ProductCodeHiddenField.Value;

            bool _result = this._airLineBL.AddAirLine(_msAirline);

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

        protected void SupplierTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.SupplierTextBox.Text != "")
            {
                this.SupplierLabel.Text = _suppBL.GetSuppNameByCode(this.SupplierTextBox.Text);
            }
            else
            {
                this.SupplierLabel.Text = " ";
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