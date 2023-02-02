using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.Supplier
{
    public partial class SupplierEdit : SupplierBase
    {
        private SupplierBL _supplierBL = new SupplierBL();
        private CityBL _cityBL = new CityBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private TermBL _termBL = new TermBL();
        private BankBL _bankBL = new BankBL();
        private PaymentTradeBL _paymentTradeBL = new PaymentTradeBL();
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
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.ShowSuppTypeDropdownlist();
                this.ShowSuppGroupDropdownlist();
                this.ShowCityDropdownlist();
                this.ShowCurrencyDropdownlist();
                this.ShowTermDropdownlist();
                this.ShowBankDropdownlist();

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void SetAttribute()
        {
            //this.ViewDetailButton.Attributes.Add("OnClick", "return AskYouFirstToSave(" + this.CheckHidden.ClientID + ");");

            this.ZipCodeTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.FgPPNCheckBox.Attributes.Add("OnClick", "CheckUncheckPPN(" + this.FgPPNCheckBox.ClientID + "," + this.NPWPTextBox.ClientID + "," + this.NPPKPTextBox.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
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

        protected void ShowData()
        {
            MsSupplier _msSupplier = this._supplierBL.GetSingleSupp(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.SuppCodeTextBox.Text = _msSupplier.SuppCode;
            this.SuppNameTextBox.Text = _msSupplier.SuppName;
            this.SupplierTypeDropDownList.SelectedValue = _msSupplier.SuppType;
            this.SupplierGroupDropDownList.SelectedValue = _msSupplier.SuppGroup;
            this.AddressTextBox.Text = _msSupplier.Address1;
            this.AddressTextBox2.Text = _msSupplier.Address2;
            this.CityDropDownList.SelectedValue = _msSupplier.City;
            this.ZipCodeTextBox.Text = _msSupplier.PostCode;
            this.TelephoneTextBox.Text = _msSupplier.Telephone;
            this.FaxTextBox.Text = _msSupplier.Fax;
            this.EmailTextBox.Text = _msSupplier.Email;
            this.CurrencyDropDownList.SelectedValue = _msSupplier.CurrCode;
            this.TermDropDownList.SelectedValue = _msSupplier.Term;
            this.BankDropDownList.SelectedValue = _msSupplier.Bank;
            this.NPWPTextBox.Text = _msSupplier.NPWP;
            this.NPPKPTextBox.Text = _msSupplier.NPPKP;
            this.ContactPersonTextBox.Text = _msSupplier.ContactPerson;
            this.ContactTitleTextBox.Text = _msSupplier.ContactTitle;
            this.ContactHPTextBox.Text = _msSupplier.ContactHP;
            this.RemarkTextBox.Text = _msSupplier.Remark;
            this.FgPPNCheckBox.Checked = SupplierDataMapper.IsPPN(_msSupplier.FgPPN);
            this.FgActiveCheckBox.Checked = SupplierDataMapper.IsActive(_msSupplier.FgActive);

            if (_msSupplier.FgPPN == SupplierDataMapper.IsPPN(false))
            {
                NPWPTextBox.Text = "";
                NPWPTextBox.Attributes.Add("ReadOnly", "True");
                NPWPTextBox.Attributes.Add("style", "background-color:#CCCCCC");
                NPPKPTextBox.Text = "";
                NPPKPTextBox.Attributes.Add("ReadOnly", "True");
                NPPKPTextBox.Attributes.Add("style", "background-color:#CCCCCC");
            }
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsSupplier _msSupplier = this._supplierBL.GetSingleSupp(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msSupplier.SuppName = this.SuppNameTextBox.Text;
            _msSupplier.SuppType = this.SupplierTypeDropDownList.SelectedValue;
            _msSupplier.SuppGroup = this.SupplierGroupDropDownList.SelectedValue;
            _msSupplier.Address1 = this.AddressTextBox.Text;
            _msSupplier.Address2 = this.AddressTextBox2.Text;
            _msSupplier.City = this.CityDropDownList.SelectedValue;
            _msSupplier.PostCode = this.ZipCodeTextBox.Text;
            _msSupplier.Telephone = this.TelephoneTextBox.Text;
            _msSupplier.Fax = this.FaxTextBox.Text;
            _msSupplier.Email = this.EmailTextBox.Text;
            _msSupplier.CurrCode = this.CurrencyDropDownList.SelectedValue;
            _msSupplier.Term = this.TermDropDownList.SelectedValue;
            _msSupplier.Bank = this.BankDropDownList.SelectedValue;
            _msSupplier.FgPPN = SupplierDataMapper.IsPPN(this.FgPPNCheckBox.Checked);
            _msSupplier.NPWP = this.NPWPTextBox.Text;
            _msSupplier.NPPKP = this.NPPKPTextBox.Text;
            _msSupplier.ContactPerson = this.ContactPersonTextBox.Text;
            _msSupplier.ContactTitle = this.ContactTitleTextBox.Text;
            _msSupplier.ContactHP = this.ContactHPTextBox.Text;
            _msSupplier.Remark = this.RemarkTextBox.Text;
            _msSupplier.FgActive = SupplierDataMapper.IsActive(this.FgActiveCheckBox.Checked);
            _msSupplier.CreatedBy = HttpContext.Current.User.Identity.Name;
            _msSupplier.CreatedDate = DateTime.Now;

            if (this._supplierBL.EditSupp(_msSupplier) == true)
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

        protected void SupplierGroupDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            MsSupplier _msSupplier = this._supplierBL.GetSingleSupp(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_paymentTradeBL.IsSuppCodeExists(this.SuppCodeTextBox.Text) == true)
            {
                this.SupplierGroupDropDownList.Enabled = false;
                this.WarningLabel.Text = "Can't Change Supplier Group Because Supplier Is Already in APPosting";
                this.SupplierGroupDropDownList.SelectedValue = _msSupplier.SuppGroup;
            }
            else
            {
                this.SupplierGroupDropDownList.Enabled = true;
            }
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {

            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));

        }
        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid == true)
            {
                MsSupplier _msSupplier = this._supplierBL.GetSingleSupp(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _msSupplier.SuppName = this.SuppNameTextBox.Text;
                _msSupplier.SuppType = this.SupplierTypeDropDownList.SelectedValue;
                _msSupplier.SuppGroup = this.SupplierGroupDropDownList.SelectedValue;
                _msSupplier.Address1 = this.AddressTextBox.Text;
                _msSupplier.Address2 = this.AddressTextBox2.Text;
                _msSupplier.City = this.CityDropDownList.SelectedValue;
                _msSupplier.PostCode = this.ZipCodeTextBox.Text;
                _msSupplier.Telephone = this.TelephoneTextBox.Text;
                _msSupplier.Fax = this.FaxTextBox.Text;
                _msSupplier.Email = this.EmailTextBox.Text;
                _msSupplier.CurrCode = this.CurrencyDropDownList.SelectedValue;
                _msSupplier.Term = this.TermDropDownList.SelectedValue;
                _msSupplier.Bank = this.BankDropDownList.SelectedValue;
                _msSupplier.FgPPN = SupplierDataMapper.IsPPN(this.FgPPNCheckBox.Checked);
                _msSupplier.NPWP = this.NPWPTextBox.Text;
                _msSupplier.NPPKP = this.NPPKPTextBox.Text;
                _msSupplier.ContactPerson = this.ContactPersonTextBox.Text;
                _msSupplier.ContactTitle = this.ContactTitleTextBox.Text;
                _msSupplier.ContactHP = this.ContactHPTextBox.Text;
                _msSupplier.Remark = this.RemarkTextBox.Text;
                _msSupplier.FgActive = SupplierDataMapper.IsActive(this.FgActiveCheckBox.Checked);
                _msSupplier.CreatedBy = HttpContext.Current.User.Identity.Name;
                _msSupplier.CreatedDate = DateTime.Now;

                if (this._supplierBL.EditSupp(_msSupplier) == true)
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "Your Failed Edit Data";
                }
            }
        }
    }
}