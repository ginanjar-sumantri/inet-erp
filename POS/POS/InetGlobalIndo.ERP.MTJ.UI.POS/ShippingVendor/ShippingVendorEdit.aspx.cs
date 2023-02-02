using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using System.Web.UI.WebControls;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingVendor
{
    public partial class ShippingVendorEdit : ShippingVendorBase
    {
        private VendorBL _vendorBL = new VendorBL();
        private PermissionBL _permBL = new PermissionBL();
        private CityBL _cityBL = new CityBL();
        private BankBL _bankBL = new BankBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private TermBL _termBL = new TermBL();
        private SupplierBL _supplierBL = new SupplierBL();

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
                this.CodeTextBox.Attributes.Add("ReadOnly", "True");
                this.PostCodeTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.TelephoneTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.FaxTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.ContactHPTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.CommissionPercentageTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.ClearLabel();
                this.ShowCity();
                this.ShowCurrency();
                this.ShowBank();
                this.ShowTerm();
                this.ShowSupplier();
                this.ShowData();
            }
        }

        protected void ShowCity()
        {
            this.CityDropDownList.Items.Clear();
            this.CityDropDownList.DataTextField = "CityName";
            this.CityDropDownList.DataValueField = "CityCode";
            this.CityDropDownList.DataSource = this._cityBL.GetListCityForDDL();
            this.CityDropDownList.DataBind();
            this.CityDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCurrency()
        {
            this.CurrCodeDropDownList.Items.Clear();
            this.CurrCodeDropDownList.DataTextField = "CurrName";
            this.CurrCodeDropDownList.DataValueField = "CurrCode";
            this.CurrCodeDropDownList.DataSource = this._currencyBL.GetListCurrForDDL();
            this.CurrCodeDropDownList.DataBind();
            this.CurrCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowBank()
        {
            this.BankDropDownList.Items.Clear();
            this.BankDropDownList.DataTextField = "BankName";
            this.BankDropDownList.DataValueField = "BankCode";
            this.BankDropDownList.DataSource = this._bankBL.GetList();
            this.BankDropDownList.DataBind();
            this.BankDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowTerm()
        {
            this.TermDropDownList.Items.Clear();
            this.TermDropDownList.DataTextField = "TermName";
            this.TermDropDownList.DataValueField = "TermCode";
            this.TermDropDownList.DataSource = this._termBL.GetListTermForDDL();
            this.TermDropDownList.DataBind();
            this.TermDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowSupplier()
        {
            this.SupplierDDL.Items.Clear();
            this.SupplierDDL.DataTextField = "SuppName";
            this.SupplierDDL.DataValueField = "SuppCode";
            this.SupplierDDL.DataSource = this._supplierBL.GetListDDLSupp();
            this.SupplierDDL.DataBind();
            this.SupplierDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            POSMsShippingVendor _posMsShippingVendor = this._vendorBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CodeTextBox.Text = _posMsShippingVendor.VendorCode.ToString();
            this.SupplierDDL.SelectedValue = _posMsShippingVendor.SupCode.ToString();
            this.NameTextBox.Text = _posMsShippingVendor.VendorName.ToString();
            this.Address1TextBox.Text = _posMsShippingVendor.Address1.ToString();
            this.Address2TextBox.Text = _posMsShippingVendor.Address2.ToString();
            this.CityDropDownList.SelectedValue = _posMsShippingVendor.City.ToString();
            this.PostCodeTextBox.Text = _posMsShippingVendor.PostCode.ToString();
            this.TelephoneTextBox.Text = _posMsShippingVendor.Telephone.ToString();
            this.FaxTextBox.Text = _posMsShippingVendor.Fax.ToString();
            this.EmailTextBox.Text = _posMsShippingVendor.Email.ToString();
            this.CurrCodeDropDownList.SelectedValue = _posMsShippingVendor.CurrCode.ToString();
            this.TermDropDownList.SelectedValue = _posMsShippingVendor.Term.ToString();
            this.BankDropDownList.SelectedValue = _posMsShippingVendor.Bank.ToString();
            this.RekeningNoTextBox.Text = _posMsShippingVendor.RekeningNo.ToString();
            this.NPWPTextBox.Text = _posMsShippingVendor.NPWP.ToString();
            if (_posMsShippingVendor.FgPPN == 'Y')
                this.FgPPnCheckBox.Checked = true;
            else
                this.FgPPnCheckBox.Checked = false;
            this.NPPKPTextBox.Text = _posMsShippingVendor.NPPKP.ToString();
            this.ContactPersonTextBox.Text = _posMsShippingVendor.ContactPerson.ToString();
            this.ContactTitleTextBox.Text = _posMsShippingVendor.ContactTitle.ToString();
            this.ContactHPTextBox.Text = _posMsShippingVendor.ContactHP.ToString();
            if (_posMsShippingVendor.FgZone == 'Y')
                this.FgZoneCheckBox.Checked = true;
            else
                this.FgZoneCheckBox.Checked = false;
            
            if (_posMsShippingVendor.FgIntPriority == 'Y')
                this.FgIntPriorityCheckBox.Checked = true;
            else
                this.FgIntPriorityCheckBox.Checked = false;
            
            if (_posMsShippingVendor.FgActive == 'Y')
                this.FgActiveCheckBox.Checked = true;
            else
                this.FgActiveCheckBox.Checked = false;
            this.CommissionPercentageTextBox.Text = Convert.ToDecimal(_posMsShippingVendor.CommissionPercentage).ToString("#0.00");
            this.RemarkTextBox.Text = _posMsShippingVendor.Remark.ToString();

        }

        protected void CheckValidData()
        {
            String _errorMsg = "";
            this.ClearLabel();

            if (this.SupplierDDL.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Supplier.";

            if (this.CityDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of City.";

            if (this.CurrCodeDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Currency.";

            if (this.TermDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Term.";

            if (this.BankDropDownList.SelectedValue == "null")
                _errorMsg = _errorMsg + " Please Choose One of Bank.";

            this.WarningLabel.Text = _errorMsg.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            this.CheckValidData();
            if (this.WarningLabel.Text == "")
            {
                POSMsShippingVendor _posMsShippingVendor = this._vendorBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                _posMsShippingVendor.VendorName = this.NameTextBox.Text;
                _posMsShippingVendor.SupCode = this.SupplierDDL.SelectedValue;
                _posMsShippingVendor.Address1 = this.Address1TextBox.Text;
                _posMsShippingVendor.Address2 = this.Address2TextBox.Text;
                _posMsShippingVendor.City = this.CityDropDownList.SelectedValue;
                _posMsShippingVendor.PostCode = this.PostCodeTextBox.Text;
                _posMsShippingVendor.Telephone = this.TelephoneTextBox.Text;
                _posMsShippingVendor.Fax = this.FaxTextBox.Text;
                _posMsShippingVendor.Email = this.EmailTextBox.Text;
                _posMsShippingVendor.CurrCode = this.CurrCodeDropDownList.SelectedValue;
                _posMsShippingVendor.Term = this.TermDropDownList.SelectedValue;
                _posMsShippingVendor.Bank = this.BankDropDownList.SelectedValue;
                _posMsShippingVendor.RekeningNo = this.RekeningNoTextBox.Text;
                _posMsShippingVendor.NPPKP = this.NPWPTextBox.Text;
                if (this.FgPPnCheckBox.Checked == true)
                    _posMsShippingVendor.FgPPN = 'Y';
                else
                    _posMsShippingVendor.FgPPN = 'N';
                _posMsShippingVendor.NPPKP = this.NPPKPTextBox.Text;
                _posMsShippingVendor.ContactPerson = this.ContactPersonTextBox.Text;
                _posMsShippingVendor.ContactTitle = this.ContactTitleTextBox.Text;
                _posMsShippingVendor.ContactHP = this.ContactHPTextBox.Text;
                if (this.FgZoneCheckBox.Checked == true)
                    _posMsShippingVendor.FgZone = 'Y';
                else
                    _posMsShippingVendor.FgZone = 'N';

                if (this.FgIntPriorityCheckBox.Checked == true)
                    _posMsShippingVendor.FgIntPriority = 'Y';
                else
                    _posMsShippingVendor.FgIntPriority = 'N'; 

                if (this.FgActiveCheckBox.Checked == true)
                    _posMsShippingVendor.FgActive = 'Y';
                else
                    _posMsShippingVendor.FgActive = 'N';
                _posMsShippingVendor.CommissionPercentage = Convert.ToDecimal(this.CommissionPercentageTextBox.Text);
                _posMsShippingVendor.Remark = this.RemarkTextBox.Text;
                _posMsShippingVendor.ModifiedBy = HttpContext.Current.User.Identity.Name;
                _posMsShippingVendor.ModifiedDate = DateTime.Now;

                bool _result = this._vendorBL.EditSubmit();

                if (_result == true)
                {
                    Response.Redirect(this._homePage);
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Edit Data";
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}