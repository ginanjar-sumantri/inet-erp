using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.CustBillingAccountBase
{
    public partial class CustBillingAccountView : CustBillingAccountBase
    {
        private CustBillAccountBL _custBillAccountBL = new CustBillAccountBL();
        private CustomerBL _custBL = new CustomerBL();
        private ProductBL _prodBL = new ProductBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private AccountBL _accountBL = new AccountBL();
        private PermissionBL _permBL = new PermissionBL();

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

        private void ShowData()
        {
            Master_CustBillAccount _msCustBillAccount = this._custBillAccountBL.GetSingleCustBillAccount(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_msCustBillAccount.CurrCode);

            this.CustBillAccountTextBox.Text = _msCustBillAccount.CustBillAccount;

            this.CustTextBox.Text = _custBL.GetNameByCode(_msCustBillAccount.CustCode) + " - " + _msCustBillAccount.CustCode;

            this.ProductNameTextBox.Text = _prodBL.GetProductNameByCode(_msCustBillAccount.ProductCode);
            this.DescTextBox.Text = _msCustBillAccount.CustBillDescription;
            this.CurrTextBox.Text = _msCustBillAccount.CurrCode;
            this.AccountCodeTextBox.Text = _msCustBillAccount.Account;
            this.AccountNameTextBox.Text = this._accountBL.GetAccountNameByCode(_msCustBillAccount.Account);
            this.AmountTextBox.Text = (_msCustBillAccount.AmountForex == 0) ? "0" : Convert.ToDecimal(_msCustBillAccount.AmountForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.BandwidthIixTextBox.Text = (_msCustBillAccount.BandWidthIIX == null) ? "" : _msCustBillAccount.BandWidthIIX.ToString();
            this.BandwidthIntTextBox.Text = (_msCustBillAccount.BandWidthINT == null) ? "" : _msCustBillAccount.BandWidthINT.ToString();
            this.BankAccountTextBox.Text = _paymentBL.GetPaymentName(_msCustBillAccount.BankAccountId);
            this.TypePaymentTextBox.Text = CustBillAccountDataMapper.GetTypePaymentText(CustBillAccountDataMapper.GetTypePayment(_msCustBillAccount.TypePayment));
            this.ActivateDateTextBox.Text = (_msCustBillAccount.ActivateDate == null) ? "" : DateFormMapper.GetValue(_msCustBillAccount.ActivateDate);
            this.ExpiredDateTextBox.Text = (_msCustBillAccount.ExpiredDate == null) ? "" : DateFormMapper.GetValue(_msCustBillAccount.ExpiredDate);

            this.ContractNoTextBox.Text = _msCustBillAccount.ContractNo;
            this.BANoTextBox.Text = _msCustBillAccount.BANo;
            // this.RatioTextBox.Text = (_msCustBillAccount.Ratio == null) ? "" : _msCustBillAccount.Ratio.ToString();
            this.RatioIixTextBox.Text = (_msCustBillAccount.RatioIIX == null) ? "" : _msCustBillAccount.RatioIIX.ToString();
            this.RatioIntTextBox.Text = (_msCustBillAccount.RatioINT == null) ? "" : _msCustBillAccount.RatioINT.ToString();
            this.FgActiveCheckBox.Checked = (_msCustBillAccount.fgActive == null) ? false : Convert.ToBoolean(_msCustBillAccount.fgActive);
            this.PeriodTextBox.Text = (_msCustBillAccount.Period == null) ? "" : _msCustBillAccount.Period.ToString();
            this.YearTextBox.Text = (_msCustBillAccount.Year == null) ? "" : _msCustBillAccount.Year.ToString();
            this.IntervalTextBox.Text = (_msCustBillAccount.Interval == null) ? "" : _msCustBillAccount.Interval.ToString();
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

