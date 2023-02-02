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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptTrade
{
    public partial class ReceiptTradeCrView : ReceiptTradeBase
    {
        private FINReceiptTradeBL _receiptTradeBL = new FINReceiptTradeBL();
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

                FINReceiptTradeHd _finReceiptTradeHd = this._receiptTradeBL.GetSingleFINReceiptTradeHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
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
            FINReceiptTradeCr _finReceiptTradeCr = this._receiptTradeBL.GetSingleFINReceiptTradeCr(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currBL.GetDecimalPlace(_finReceiptTradeCr.CurrCode);
            byte _decimalPlace2 = _currBL.GetDecimalPlace(_currBL.GetCurrDefault());

            this.InvoiceNoTextBox.Text = _receiptTradeBL.GetFileNmbrFINARPostingByInvoiceNo(_finReceiptTradeCr.InvoiceNo);
            this.CurrCodeTextBox.Text = _finReceiptTradeCr.CurrCode;
            this.ForexRateTextBox.Text = (_finReceiptTradeCr.ForexRate == 0) ? "0" : _finReceiptTradeCr.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.ARInvoiceTextBox.Text = (_finReceiptTradeCr.ARInvoice == 0) ? "0" : _finReceiptTradeCr.ARInvoice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.ARBalanceTextBox.Text = (_finReceiptTradeCr.ARBalance == 0) ? "0" : _finReceiptTradeCr.ARBalance.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.ARToBePaidTextBox.Text = (_finReceiptTradeCr.ARInvoice - _finReceiptTradeCr.ARBalance == 0) ? "0" : (_finReceiptTradeCr.ARInvoice - _finReceiptTradeCr.ARBalance).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _ppnRate = Convert.ToDecimal((_finReceiptTradeCr.PPnRate == null) ? 0 : _finReceiptTradeCr.PPnRate);
            this.PPNRateTextBox.Text = (_ppnRate == 0) ? "0" : _ppnRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNInvoiceTextBox.Text = (_finReceiptTradeCr.PPnInvoice == 0) ? "0" : _finReceiptTradeCr.PPnInvoice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNBalanceTextBox.Text = (_finReceiptTradeCr.PPnBalance == 0) ? "0" : _finReceiptTradeCr.PPnBalance.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNToBePaidTextBox.Text = (_finReceiptTradeCr.PPnInvoice - _finReceiptTradeCr.PPnBalance == 0) ? "0" : (_finReceiptTradeCr.PPnInvoice - _finReceiptTradeCr.PPnBalance).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.AmountInvoiceTextBox.Text = (_finReceiptTradeCr.AmountInvoice == 0) ? "0" : _finReceiptTradeCr.AmountInvoice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountBalanceTextBox.Text = (_finReceiptTradeCr.AmountBalance == 0) ? "0" : _finReceiptTradeCr.AmountBalance.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountToBePaidTextBox.Text = (_finReceiptTradeCr.AmountInvoice - _finReceiptTradeCr.AmountBalance == 0) ? "0" : (_finReceiptTradeCr.AmountInvoice - _finReceiptTradeCr.AmountBalance).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.ReceiptAmountForexTextBox.Text = (_finReceiptTradeCr.ARPaid == 0) ? "0" : _finReceiptTradeCr.ARPaid.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.ReceiptPPNTextBox.Text = (_finReceiptTradeCr.PPnPaid == 0) ? "0" : _finReceiptTradeCr.PPnPaid.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.ReceiptAmountBalanceTextBox.Text = ((_finReceiptTradeCr.ARInvoice - _finReceiptTradeCr.ARBalance) - _finReceiptTradeCr.ARPaid == 0) ? "0" : ((_finReceiptTradeCr.ARInvoice - _finReceiptTradeCr.ARBalance) - _finReceiptTradeCr.ARPaid).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.ReceiptPPNBalanceTextBox.Text = ((_finReceiptTradeCr.PPnInvoice - _finReceiptTradeCr.PPnBalance) - _finReceiptTradeCr.PPnPaid == 0) ? "0" : ((_finReceiptTradeCr.PPnInvoice - _finReceiptTradeCr.PPnBalance) - _finReceiptTradeCr.PPnPaid).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)); ;

            decimal _totalAmount = _finReceiptTradeCr.ARPaid + _finReceiptTradeCr.PPnPaid;
            this.TotalAmountForexTextBox.Text = (_totalAmount == 0) ? "0" : _totalAmount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _totalAmountBalance = (_finReceiptTradeCr.AmountInvoice - _finReceiptTradeCr.AmountBalance) - _totalAmount;
            this.TotalAmountBalanceTextBox.Text = (_totalAmountBalance == 0) ? "0" : _totalAmountBalance.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.RemarkTextBox.Text = _finReceiptTradeCr.Remark;
            decimal _value = Convert.ToDecimal((_finReceiptTradeCr.FgValue == null) ? 0 : _finReceiptTradeCr.FgValue);
            this.FgValueTextBox.Text = (_value == 0) ? "0" : _value.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
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