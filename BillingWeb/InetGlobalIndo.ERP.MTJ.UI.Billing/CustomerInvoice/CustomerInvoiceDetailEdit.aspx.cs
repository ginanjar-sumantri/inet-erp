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
    public partial class CustomerInvoiceDetailEdit : CustomerInvoiceBase
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

                this.ClearLabel();
                this.SetAttribute();

                this.ShowAccount();
                this.ShowCustomerBillAcount();
                this.ShowBankDropdownlist();
                this.ShowData();
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

        protected void ShowData()
        {
            this.ClearLabel();

            string _invServHd = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _currCode = this._customerInvoiceBL.GetCurrCodeFromCustomerInvoiceHd(new Guid(_invServHd));

            string _transNo = this._customerInvoiceBL.GetTransactionNoFromCustomerInvoiceHd(new Guid(_invServHd));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            string _invServDt = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey);
            Billing_CustomerInvoiceDt _customerInvoiceDt = this._customerInvoiceBL.GetSingleCustomerInvoiceDt(new Guid(_invServDt));

            this.AccountTextBox.Text = _customerInvoiceDt.Account;
            this.AccountDropDownList.SelectedValue = _customerInvoiceDt.Account;
            this.BankAccountDropDownList.SelectedValue = _customerInvoiceDt.BankAccountId;

            this.CustomerInvoiceDescriptionTextBox.Text = _customerInvoiceDt.CustInvoiceDescription;
            this.AmountForexTextBox.Text = (_customerInvoiceDt.AmountForex == 0) ? "0" : _customerInvoiceDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _customerInvoiceDt.Remark;

            if (this._customerInvoiceBL.GetTypeCustomerInvoiceHd(new Guid(_invServHd)) != CustomerInvoiceDataMapper.GetType(CustomerInvoiceType.Postpone))
            {
                this.CustomerBillAccount.Visible = false;
                this.CustomerBillAccountDropDownList.Visible = false;
                this.CustomerBillAccountCustomValidator.Enabled = false;
            }
            else
            {
                Billing_CustomerInvoiceDtExtension _customerInvoiceDtExtension = this._customerInvoiceBL.GetSingleCustomerInvoiceDtExtension(new Guid(_invServDt));
                this.CustomerBillAccountDropDownList.SelectedValue = _customerInvoiceDtExtension.CustBillCode.ToString();
            }

            this.SetAttributeRate();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _invServHd = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _invServDt = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey);
            Billing_CustomerInvoiceDt _customerInvoiceDt = this._customerInvoiceBL.GetSingleCustomerInvoiceDt(new Guid(_invServDt));

            _customerInvoiceDt.Account = this.AccountDropDownList.SelectedValue;
            _customerInvoiceDt.BankAccountId = this.BankAccountDropDownList.SelectedValue;
            _customerInvoiceDt.CustInvoiceDescription = this.CustomerInvoiceDescriptionTextBox.Text;
            _customerInvoiceDt.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _customerInvoiceDt.Remark = this.RemarkTextBox.Text;

            Billing_CustomerInvoiceDtExtension _customerInvoiceDtExtension = null;
            if (this._customerInvoiceBL.GetTypeCustomerInvoiceHd(new Guid(_invServHd)) == CustomerInvoiceDataMapper.GetType(CustomerInvoiceType.Postpone))
            {
                _customerInvoiceDtExtension = new Billing_CustomerInvoiceDtExtension();
                _customerInvoiceDtExtension.CustomerInvoiceDtCode = _customerInvoiceDt.CustomerInvoiceDtCode;
                _customerInvoiceDtExtension.CustBillCode = new Guid(this.CustomerBillAccountDropDownList.SelectedValue);

                _customerInvoiceDtExtension.InsertBy = HttpContext.Current.User.Identity.Name;
                _customerInvoiceDtExtension.InsertDate = DateTime.Now;
                _customerInvoiceDtExtension.EditBy = HttpContext.Current.User.Identity.Name;
                _customerInvoiceDtExtension.EditDate = DateTime.Now;
            }

            bool _result = this._customerInvoiceBL.EditCustomerInvoiceDt(_customerInvoiceDt, _customerInvoiceDtExtension);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}