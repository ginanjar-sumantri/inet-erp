using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.CustomerInvoice
{
    public partial class CustomerInvoiceDetailAdd : CustomerInvoiceBase
    {
        private CustomerInvoiceBL _customerInvoiceBL = new CustomerInvoiceBL();
        private CustBillAccountBL _custBillAccountBL = new CustBillAccountBL();
        private AccountBL _accountBL = new AccountBL();
        private SubledBL _subledBL = new SubledBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearLabel();
                this.SetAttribute();

                this.ShowAccount();
                this.ShowCustomerBillAcount();
                this.ShowBankDropdownlist();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.AmountForexTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.AmountForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void SetAttribute()
        {
            this.AccountDropDownList.Attributes.Add("OnChange", "Selected(" + this.AccountDropDownList.ClientID + "," + this.AccountTextBox.ClientID + ");");
            this.AccountTextBox.Attributes.Add("OnBlur", "Blur(" + this.AccountDropDownList.ClientID + "," + this.AccountTextBox.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");

            this.SetAttributeRate();
        }

        protected void ShowAccount()
        {
            string _invServHd = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _currCode = this._customerInvoiceBL.GetCurrCodeFromCustomerInvoiceHd(new Guid(_invServHd));

            this.AccountDropDownList.DataTextField = "AccountName";
            this.AccountDropDownList.DataValueField = "Account";
            this.AccountDropDownList.DataSource = _accountBL.GetListForDDLSpecTransaction(AppModule.GetValue(TransactionType.CustomerInvoice), SubledDataMapper.GetSubled(SubledStatus.Customer), _currCode);
            this.AccountDropDownList.DataBind();
            this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCustomerBillAcount()
        {
            string _invServHd = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _custCode = this._customerInvoiceBL.GetCustCodeFromCustomerInvoiceHd(new Guid(_invServHd));
            string _currCode = this._customerInvoiceBL.GetCurrCodeFromCustomerInvoiceHd(new Guid(_invServHd));

            this.CustomerBillAccountDropDownList.Items.Clear();
            this.CustomerBillAccountDropDownList.DataTextField = "CustBillAccount";
            this.CustomerBillAccountDropDownList.DataValueField = "CustBillCode";
            this.CustomerBillAccountDropDownList.DataSource = this._custBillAccountBL.GetListDDLCustBillAccountPostpone(_custCode, _currCode);
            this.CustomerBillAccountDropDownList.DataBind();
            this.CustomerBillAccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowBankDropdownlist()
        {
            string _invServHd = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _currCode = this._customerInvoiceBL.GetCurrCodeFromCustomerInvoiceHd(new Guid(_invServHd));

            this.BankAccountDropDownList.Items.Clear();
            this.BankAccountDropDownList.DataSource = this._paymentBL.GetListDDLCustBillAcc(_currCode);
            this.BankAccountDropDownList.DataValueField = "PayCode";
            this.BankAccountDropDownList.DataTextField = "PayName";
            this.BankAccountDropDownList.DataBind();
            this.BankAccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ClearData()
        {
            this.ClearLabel();

            string _invServHd = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _currCode = this._customerInvoiceBL.GetCurrCodeFromCustomerInvoiceHd(new Guid(_invServHd));

            string _transNo = this._customerInvoiceBL.GetTransactionNoFromCustomerInvoiceHd(new Guid(_invServHd));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.AccountTextBox.Text = "";
            this.AccountDropDownList.SelectedValue = "null";
            this.BankAccountDropDownList.SelectedValue = "null";
            this.CustomerInvoiceDescriptionTextBox.Text = "";
            this.AmountForexTextBox.Text = "0";
            this.RemarkTextBox.Text = "";

            if (this._customerInvoiceBL.GetTypeCustomerInvoiceHd(new Guid(_invServHd)) != CustomerInvoiceDataMapper.GetType(CustomerInvoiceType.Postpone))
            {
                this.CustomerBillAccount.Visible = false;
                this.CustomerBillAccountDropDownList.Visible = false;
                this.CustomerBillAccountCustomValidator.Enabled = false;
            }

            this.SetAttributeRate();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _invoiceHd = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            Billing_CustomerInvoiceDt _customerInvoiceDt = new Billing_CustomerInvoiceDt();

            _customerInvoiceDt.CustomerInvoiceDtCode = Guid.NewGuid();
            _customerInvoiceDt.CustomerInvoiceHdCode = new Guid(_invoiceHd);
            _customerInvoiceDt.Account = this.AccountDropDownList.SelectedValue;
            _customerInvoiceDt.BankAccountId = this.BankAccountDropDownList.SelectedValue;
            _customerInvoiceDt.CustInvoiceDescription = this.CustomerInvoiceDescriptionTextBox.Text;
            _customerInvoiceDt.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _customerInvoiceDt.Remark = this.RemarkTextBox.Text;

            Billing_CustomerInvoiceDtExtension _customerInvoiceDtExtension = null;
            if (this._customerInvoiceBL.GetTypeCustomerInvoiceHd(new Guid(_invoiceHd)) == CustomerInvoiceDataMapper.GetType(CustomerInvoiceType.Postpone))
            {
                _customerInvoiceDtExtension = new Billing_CustomerInvoiceDtExtension();
                _customerInvoiceDtExtension.CustomerInvoiceDtCode = _customerInvoiceDt.CustomerInvoiceDtCode;
                _customerInvoiceDtExtension.CustBillCode = new Guid(this.CustomerBillAccountDropDownList.SelectedValue);

                _customerInvoiceDtExtension.InsertBy = HttpContext.Current.User.Identity.Name;
                _customerInvoiceDtExtension.InsertDate = DateTime.Now;
                _customerInvoiceDtExtension.EditBy = HttpContext.Current.User.Identity.Name;
                _customerInvoiceDtExtension.EditDate = DateTime.Now;
            }

            bool _result = this._customerInvoiceBL.AddCustomerInvoiceDt(_customerInvoiceDt, _customerInvoiceDtExtension);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }
        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}