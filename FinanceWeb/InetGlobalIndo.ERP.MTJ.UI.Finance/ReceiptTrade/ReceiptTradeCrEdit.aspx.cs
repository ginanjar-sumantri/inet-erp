using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptTrade
{
    public partial class ReceiptTradeCrEdit : ReceiptTradeBase
    {
        private FINReceiptTradeBL _receiptTrade = new FINReceiptTradeBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currBL = new CurrencyBL();

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
                this.ShowData();
                this.SetAttribute();
            }
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.CurrCodeTextBox.Attributes.Add("ReadOnly", "True");
            this.ForexRateTextBox.Attributes.Add("ReadOnly", "True");
            this.ForexRateTextBox.Attributes.Add("Style", "background-color:#CCCCCC");
            this.FgValueTextBox.Attributes.Add("ReadOnly", "True");

            this.ARInvoiceTextBox.Attributes.Add("ReadOnly", "True");
            this.ARBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.ARToBePaidTextBox.Attributes.Add("ReadOnly", "True");

            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNInvoiceTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNToBePaidTextBox.Attributes.Add("ReadOnly", "True");

            this.AmountInvoiceTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountToBePaidTextBox.Attributes.Add("ReadOnly", "True");

            this.ReceiptAmountBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.ReceiptPPNBalanceTextBox.Attributes.Add("ReadOnly", "True");

            this.TotalAmountBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalAmountForexTextBox.Attributes.Add("ReadOnly", "True");

            this.ForexRateTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.ReceiptPPNTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.ReceiptPPNTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
            this.ReceiptAmountForexTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.ReceiptAmountForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");

            this.ReceiptAmountForexTextBox.Attributes.Add("OnBlur", "CalculateAmountBalance(" + this.ARToBePaidTextBox.ClientID + "," + this.PPNToBePaidTextBox.ClientID + "," + this.AmountToBePaidTextBox.ClientID + "," + this.ReceiptAmountForexTextBox.ClientID + "," + this.ReceiptAmountBalanceTextBox.ClientID + "," + this.ReceiptPPNTextBox.ClientID + "," + this.ReceiptPPNBalanceTextBox.ClientID + "," + this.TotalAmountForexTextBox.ClientID + "," + this.TotalAmountBalanceTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            this.ReceiptPPNTextBox.Attributes.Add("OnBlur", "CalculateAmountBalance(" + this.ARToBePaidTextBox.ClientID + "," + this.PPNToBePaidTextBox.ClientID + "," + this.AmountToBePaidTextBox.ClientID + "," + this.ReceiptAmountForexTextBox.ClientID + "," + this.ReceiptAmountBalanceTextBox.ClientID + "," + this.ReceiptPPNTextBox.ClientID + "," + this.ReceiptPPNBalanceTextBox.ClientID + "," + this.TotalAmountForexTextBox.ClientID + "," + this.TotalAmountBalanceTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowData()
        {
            FINReceiptTradeCr _finReceiptTradeCr = this._receiptTrade.GetSingleFINReceiptTradeCr(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currBL.GetDecimalPlace(_finReceiptTradeCr.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            byte _decimalPlace2 = _currBL.GetDecimalPlace(_currBL.GetCurrDefault());
            this.DecimalPlaceHiddenField2.Value = CurrencyDataMapper.GetDecimal(_decimalPlace2);

            this.InvoiceNoTextBox.Text = _receiptTrade.GetFileNmbrFINARPostingByInvoiceNo(_finReceiptTradeCr.InvoiceNo);
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

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINReceiptTradeCr _finReceiptTradeCr = this._receiptTrade.GetSingleFINReceiptTradeCr(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            _finReceiptTradeCr.CurrCode = this.CurrCodeTextBox.Text;
            _finReceiptTradeCr.ForexRate = Convert.ToDecimal(this.ForexRateTextBox.Text);
            _finReceiptTradeCr.ARInvoice = Convert.ToDecimal((this.ARInvoiceTextBox.Text == "") ? "0" : this.ARInvoiceTextBox.Text);
            _finReceiptTradeCr.ARBalance = Convert.ToDecimal((this.ARBalanceTextBox.Text == "") ? "0" : this.ARBalanceTextBox.Text);
            _finReceiptTradeCr.ARPaid = Convert.ToDecimal((this.ReceiptAmountForexTextBox.Text == "") ? "0" : this.ReceiptAmountForexTextBox.Text);
            _finReceiptTradeCr.PPnRate = Convert.ToDecimal((this.PPNRateTextBox.Text == "") ? "0" : this.PPNRateTextBox.Text);
            _finReceiptTradeCr.PPnInvoice = Convert.ToDecimal((this.PPNInvoiceTextBox.Text == "") ? "0" : this.PPNInvoiceTextBox.Text);
            _finReceiptTradeCr.PPnBalance = Convert.ToDecimal((this.PPNBalanceTextBox.Text == "") ? "0" : this.PPNBalanceTextBox.Text);
            _finReceiptTradeCr.PPnPaid = Convert.ToDecimal((this.ReceiptPPNTextBox.Text == "") ? "0" : this.ReceiptPPNTextBox.Text);
            _finReceiptTradeCr.AmountForex = Convert.ToDecimal((this.TotalAmountForexTextBox.Text == "") ? "0" : this.TotalAmountForexTextBox.Text);
            _finReceiptTradeCr.AmountInvoice = Convert.ToDecimal((this.AmountInvoiceTextBox.Text == "") ? "0" : this.AmountInvoiceTextBox.Text);
            _finReceiptTradeCr.AmountBalance = Convert.ToDecimal((this.AmountBalanceTextBox.Text == "") ? "0" : this.AmountBalanceTextBox.Text);
            decimal _amountHome = (_finReceiptTradeCr.ARPaid * _finReceiptTradeCr.ForexRate) + (Convert.ToDecimal(_finReceiptTradeCr.PPnRate) * _finReceiptTradeCr.PPnPaid);
            _finReceiptTradeCr.AmountHome = _amountHome;
            _finReceiptTradeCr.Remark = this.RemarkTextBox.Text;
            _finReceiptTradeCr.FgValue = Convert.ToDecimal(this.FgValueTextBox.Text);

            bool _result = this._receiptTrade.EditFINReceiptTradeCr(_finReceiptTradeCr);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}