using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentTrade
{
    public partial class PaymentTradeCrView : PaymentTradeBase
    {
        private AccountBL _accountBL = new AccountBL();
        private PaymentTradeBL _paymentTradeBL = new PaymentTradeBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currBL = new CurrencyBL();

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

                FINPayTradeHd _finPayTradeHd = this._paymentTradeBL.GetSingleFINPayTradeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                if (_finPayTradeHd.Status != PaymentTradeDataMapper.GetStatus(TransStatus.Posted))
                {
                    this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                }
                else
                {
                    this.EditButton.Visible = false;
                }
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

        public void ShowData()
        {
            FINPayTradeCr _finPayTradeCr = this._paymentTradeBL.GetSingleFINPayTradeCr(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currBL.GetDecimalPlace(_finPayTradeCr.CurrCode);
            byte _decimalPlace2 = _currBL.GetDecimalPlace(_currBL.GetCurrDefault());
            this.PayTypeTextBox.Text = _paymentBL.GetPaymentName(_finPayTradeCr.PayType);
            this.DocumentNoTextBox.Text = _finPayTradeCr.DocumentNo;
            this.CurrCodeTextBox.Text = _finPayTradeCr.CurrCode;
            this.CurrRateTextBox.Text = (_finPayTradeCr.ForexRate == 0) ? "0" : _finPayTradeCr.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountForexTextBox.Text = (_finPayTradeCr.AmountForex == 0) ? "0" : _finPayTradeCr.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountTotalTextBox.Text = (((_finPayTradeCr.AmountHome == null) ? 0 : Convert.ToDecimal(_finPayTradeCr.AmountHome)) == 0) ? "0" : Convert.ToDecimal(_finPayTradeCr.AmountHome).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace2));
            this.RemarkTextBox.Text = _finPayTradeCr.Remark;
            this.BankPaymentTextBox.Text = (_finPayTradeCr.BankPayment == "null") ? "" : _finPayTradeCr.BankPayment;
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_finPayTradeCr.DueDate);
            this.BankExpenseTextBox.Text = (((_finPayTradeCr.BankExpense == null) ? 0 : Convert.ToDecimal(_finPayTradeCr.BankExpense)) == 0) ? "0" : Convert.ToDecimal(_finPayTradeCr.BankExpense).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace2));
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage2 + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeItemKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeItemKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}