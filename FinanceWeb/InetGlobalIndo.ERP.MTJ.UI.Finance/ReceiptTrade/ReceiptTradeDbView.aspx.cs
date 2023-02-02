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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptTrade
{
    public partial class ReceiptTradeDbView : ReceiptTradeBase
    {
        private BankBL _bankBL = new BankBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private FINReceiptTradeBL _receiptBL = new FINReceiptTradeBL();
        private CurrencyRateBL _currRate = new CurrencyRateBL();
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

                FINReceiptTradeHd _finReceiptTradeHd = this._receiptBL.GetSingleFINReceiptTradeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                if (_finReceiptTradeHd.Status != ReceiptTradeDataMapper.GetStatus(TransStatus.Posted))
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
            FINReceiptTradeDb _finReceiptTradeDb = this._receiptBL.GetSingleFINReceiptTradeDb(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));
            MsPayType _msPayType = _paymentBL.GetSinglePaymentType(_finReceiptTradeDb.ReceiptType);

            this.PayTypeDropDownList.Text = this._paymentBL.GetPaymentName(_finReceiptTradeDb.ReceiptType);
            this.DocumentNoTextBox.Text = _finReceiptTradeDb.DocumentNo;
            this.CurrCodeTextBox.Text = _finReceiptTradeDb.CurrCode;
            byte _decimalPlace = _currBL.GetDecimalPlace(_finReceiptTradeDb.CurrCode);
            byte _decimalPlace2 = _currBL.GetDecimalPlace(_currBL.GetCurrDefault());
            this.CurrRateTextBox.Text = (_finReceiptTradeDb.ForexRate == 0) ? "0" : _finReceiptTradeDb.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountForexTextBox.Text = (_finReceiptTradeDb.AmountForex == 0) ? "0" : _finReceiptTradeDb.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _finReceiptTradeDb.Remark;
            this.BankGiroDropDownList.Text = this._bankBL.GetBankNameByCode(_finReceiptTradeDb.BankGiro);

            if (_finReceiptTradeDb.DueDate != null)
            {
                this.DueDateTextBox.Text = DateFormMapper.GetValue(_finReceiptTradeDb.DueDate);
            }

            if (_finReceiptTradeDb.BankExpense != null)
            {
                if (_msPayType.FgBankCharge == true)
                {
                    this.BankExpenseAmountTextBox.Text = (_msPayType.ExpenseGiro == 0) ? "0" : _msPayType.ExpenseGiro.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    this.BankExpensePercentTextBox.Text = "0";
                }
                else
                {
                    this.BankExpensePercentTextBox.Text = (_msPayType.ExpenseGiro == 0) ? "0" : _msPayType.ExpenseGiro.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                    if (_msPayType.FgCustCharge == true)
                    {
                        this.BankExpenseAmountTextBox.Text = ((_finReceiptTradeDb.AmountForex + _msPayType.CustRevenue) * _msPayType.ExpenseGiro / 100 == 0) ? "0" : ((_finReceiptTradeDb.AmountForex + _msPayType.CustRevenue) * _msPayType.ExpenseGiro / 100).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    }
                    else
                    {
                        this.BankExpenseAmountTextBox.Text = ((_finReceiptTradeDb.AmountForex + (_finReceiptTradeDb.AmountForex * _msPayType.CustRevenue / 100)) * _msPayType.ExpenseGiro / 100 == 0) ? "0" : ((_finReceiptTradeDb.AmountForex + (_finReceiptTradeDb.AmountForex * _msPayType.CustRevenue / 100)) * _msPayType.ExpenseGiro / 100).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    }
                }
            }

            if (_finReceiptTradeDb.CustRevenue != null)
            {
                if (_msPayType.FgCustCharge == true)
                {
                    this.CustRevenueAmountTextBox.Text = (_msPayType.CustRevenue == 0) ? "0" : _msPayType.CustRevenue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    this.CustRevenuePercentTextBox.Text = "0";
                }
                else
                {
                    this.CustRevenuePercentTextBox.Text = (_msPayType.CustRevenue == 0) ? "0" : _msPayType.CustRevenue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                    this.CustRevenueAmountTextBox.Text = (_finReceiptTradeDb.AmountForex * _msPayType.CustRevenue / 100 == 0) ? "0" : (_finReceiptTradeDb.AmountForex * _msPayType.CustRevenue / 100).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                }
            }
            this.TotalCustPaidTextBox.Text = Convert.ToDecimal(_finReceiptTradeDb.AmountForex + _finReceiptTradeDb.CustRevenue).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalReceiptForexTextBox.Text = (_finReceiptTradeDb.ReceiptForex == null || _finReceiptTradeDb.ReceiptForex == 0) ? "0" : Convert.ToDecimal(_finReceiptTradeDb.ReceiptForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalReceiptHomeTextBox.Text = (_finReceiptTradeDb.ReceiptHome == null || _finReceiptTradeDb.ReceiptHome == 0) ? "0" : Convert.ToDecimal(_finReceiptTradeDb.ReceiptHome).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace2));
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeItemKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeItemKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}