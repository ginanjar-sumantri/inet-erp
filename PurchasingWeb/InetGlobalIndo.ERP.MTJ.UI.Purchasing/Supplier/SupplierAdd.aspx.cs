using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.Supplier
{
    public partial class SupplierAdd : SupplierBase
    {
        private SupplierBL _supplierBL = new SupplierBL();
        private CityBL _cityBL = new CityBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private TermBL _termBL = new TermBL();
        private BankBL _bankBL = new BankBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                
                this.ShowSuppTypeDropdownlist();
                this.ShowSuppGroupDropdownlist();
                this.ShowCityDropdownlist();
                this.ShowCurrencyDropdownlist();
                this.ShowTermDropdownlist();
                this.ShowBankDropdownlist();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void SetAttribute()
        {
            this.NPWPTextBox.Attributes.Add("ReadOnly", "True");
            this.NPWPTextBox.Attributes.Add("style", "background-color:#CCCCCC");

            this.NPPKPTextBox.Attributes.Add("ReadOnly", "True");
            this.NPPKPTextBox.Attributes.Add("style", "background-color:#CCCCCC");

            this.ZipCodeTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.FgPPNCheckBox.Attributes.Add("OnClick", "CheckUncheckPPN(" + this.FgPPNCheckBox.ClientID + "," + this.NPWPTextBox.ClientID + "," + this.NPPKPTextBox.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.ClearLabel();
            this.SuppCodeTextBox.Text = "";
            this.SuppNameTextBox.Text = "";
            this.TelephoneTextBox.Text = "";
            this.SupplierTypeDropDownList.SelectedValue = "null";
            this.SupplierGroupDropDownList.SelectedValue = "null";
            this.RemarkTextBox.Text = "";
            this.FgActiveCheckBox.Checked = true;
        }

        private void ShowSuppTypeDropdownlist()
        {
            this.SupplierTypeDropDownList.DataSource = this._supplierBL.GetListSuppTypeForDDL();
            this.SupplierTypeDropDownList.DataValueField = "SuppTypeCode";
            this.SupplierTypeDropDownList.DataTextField = "SuppTypeName";
            this.SupplierTypeDropDownList.DataBind();
            this.SupplierTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowSuppGroupDropdownlist()
        {
            this.SupplierGroupDropDownList.DataSource = this._supplierBL.GetListSuppGroupForDDL();
            this.SupplierGroupDropDownList.DataValueField = "SuppGroupCode";
            this.SupplierGroupDropDownList.DataTextField = "SuppGroupName";
            this.SupplierGroupDropDownList.DataBind();
            this.SupplierGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCityDropdownlist()
        {
            this.CityDropDownList.DataSource = this._cityBL.GetListCityForDDL();
            this.CityDropDownList.DataValueField = "CityCode";
            this.CityDropDownList.DataTextField = "CityName";
            this.CityDropDownList.DataBind();
            this.CityDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowCurrencyDropdownlist()
        {
            this.CurrencyDropDownList.DataSource = this._currencyBL.GetListAll();
            this.CurrencyDropDownList.DataValueField = "CurrCode";
            this.CurrencyDropDownList.DataTextField = "CurrCode";
            this.CurrencyDropDownList.DataBind();
            this.CurrencyDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowTermDropdownlist()
        {
            this.TermDropDownList.DataSource = this._termBL.GetListTermForDDL();
            this.TermDropDownList.DataValueField = "TermCode";
            this.TermDropDownList.DataTextField = "TermName";
            this.TermDropDownList.DataBind();
            this.TermDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowBankDropdownlist()
        {
            this.BankDropDownList.DataSource = this._bankBL.GetList();
            this.BankDropDownList.DataValueField = "BankCode";
            this.BankDropDownList.DataTextField = "BankName";
            this.BankDropDownList.DataBind();
            this.BankDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsSupplier _msSupp = new MsSupplier();

            _msSupp.SuppCode = this.SuppCodeTextBox.Text;
            _msSupp.SuppName = this.SuppNameTextBox.Text;
            _msSupp.SuppType = this.SupplierTypeDropDownList.SelectedValue;
            _msSupp.SuppGroup = this.SupplierGroupDropDownList.SelectedValue;
            _msSupp.Address1 = this.AddressTextBox.Text;
            _msSupp.Address2 = this.AddressTextBox2.Text;
            _msSupp.City = this.CityDropDownList.SelectedValue;
            _msSupp.PostCode = this.ZipCodeTextBox.Text;
            _msSupp.Telephone = this.TelephoneTextBox.Text;
            _msSupp.Fax = this.FaxTextBox.Text;
            _msSupp.Email = this.EmailTextBox.Text;
            _msSupp.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _msSupp.Term = this.TermDropDownList.SelectedValue;
            _msSupp.Bank = this.BankDropDownList.SelectedValue;
            _msSupp.FgPPN = SupplierDataMapper.IsPPN(this.FgPPNCheckBox.Checked);
            _msSupp.NPWP = this.NPWPTextBox.Text;
            _msSupp.NPPKP = this.NPPKPTextBox.Text;
            _msSupp.ContactPerson = this.ContactPersonTextBox.Text;
            _msSupp.ContactTitle = this.ContactTitleTextBox.Text;
            _msSupp.ContactHP = this.ContactHPTextBox.Text;
            _msSupp.FgActive = SupplierDataMapper.IsActive(this.FgActiveCheckBox.Checked);
            _msSupp.Remark = this.RemarkTextBox.Text;
            _msSupp.CreatedBy = HttpContext.Current.User.Identity.Name;
            _msSupp.CreatedDate = DateTime.Now;

            bool _result = this._supplierBL.AddSupp(_msSupp);

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
            this.ClearData();
        }
    }
}