using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.CustomerInvoice
{
    public partial class CustomerInvoiceDetailView : CustomerInvoiceBase
    {
        private CustomerInvoiceBL _customerInvoiceBL = new CustomerInvoiceBL();
        private CustBillAccountBL _custBillAccountBL = new CustBillAccountBL();
        private AccountBL _accountBL = new AccountBL();
        private CustomerBL _customerBL = new CustomerBL();
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

                this.SetButtonPermission();
                this.ShowData();
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


        protected void ShowData()
        {
            string _invServHd = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _transNo = this._customerInvoiceBL.GetTransactionNoFromCustomerInvoiceHd(new Guid(_invServHd));

            string _currCode = this._customerInvoiceBL.GetCurrCodeFromCustomerInvoiceHd(new Guid(_invServHd));
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);

            string _invServDt = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey);
            Billing_CustomerInvoiceDt _customerInvoiceDt = this._customerInvoiceBL.GetSingleCustomerInvoiceDt(new Guid(_invServDt));

            this.AccountCodeTextBox.Text = _customerInvoiceDt.Account;
            this.AccountNameTextBox.Text = this._accountBL.GetAccountNameByCode(_customerInvoiceDt.Account);
            this.BankAccountTextBox.Text = _paymentBL.GetPaymentName(_customerInvoiceDt.BankAccountId);

            this.CustomerInvoiceDescriptionTextBox.Text = _customerInvoiceDt.CustInvoiceDescription;
            this.AmountForexTextBox.Text = (_customerInvoiceDt.AmountForex == 0) ? "0" : _customerInvoiceDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _customerInvoiceDt.Remark;

            if (this._customerInvoiceBL.GetTypeCustomerInvoiceHd(new Guid(_invServHd)) != CustomerInvoiceDataMapper.GetType(CustomerInvoiceType.Postpone))
            {
                this.CustomerBillAccount.Visible = false;
                this.CustBillAccountTextBox.Visible = false;
            }
            else
            {
                Billing_CustomerInvoiceDtExtension _customerInvoiceDtExtension = this._customerInvoiceBL.GetSingleCustomerInvoiceDtExtension(new Guid(_invServDt));
                this.CustBillAccountTextBox.Text = this._custBillAccountBL.GetCustBillAccount(_customerInvoiceDtExtension.CustBillCode);
            }

            char _status = this._customerInvoiceBL.GetStatusCustomerInvoiceHd(new Guid(_invServHd));

            if (_status == CustomerInvoiceDataMapper.GetStatus(TransStatus.Posted))
            {
                this.EditButton.Visible = false;
            }
            else
            {
                this.EditButton.Visible = true;
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeItemKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}