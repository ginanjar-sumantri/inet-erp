using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentTrade
{
    public partial class PaymentTradeDbEdit : PaymentTradeBase
    {
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
            this.APInvoiceTextBox.Attributes.Add("ReadOnly", "True");
            this.APBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.APPaidTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNRateTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNInvoiceTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountInvoiceTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountBalanceTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountHomeTextBox.Attributes.Add("ReadOnly", "True");
            this.FgValueTextBox.Attributes.Add("ReadOnly", "True");

            this.AmountForexTextBox.Attributes.Add("OnBlur", "CalculateAmountHome(" + this.AmountForexTextBox.ClientID + "," + this.ForexRateTextBox.ClientID + "," + this.PPNPaidTextBox.ClientID + "," + this.APPaidTextBox.ClientID + "," + this.PPNRateTextBox.ClientID + "," + this.AmountHomeTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");
            this.PPNPaidTextBox.Attributes.Add("OnBlur", "CalculateAmountHome(" + this.AmountForexTextBox.ClientID + "," + this.ForexRateTextBox.ClientID + "," + this.PPNPaidTextBox.ClientID + "," + this.APPaidTextBox.ClientID + "," + this.PPNRateTextBox.ClientID + "," + this.AmountHomeTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHiddenField2.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowData()
        {
            FINPayTradeDb _finPayTradeDb = this._paymentTradeBL.GetSingleFINPayTradeDb(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currBL.GetDecimalPlace(_finPayTradeDb.CurrCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            byte _decimalPlace2 = _currBL.GetDecimalPlace(_currBL.GetCurrDefault());
            this.DecimalPlaceHiddenField2.Value = CurrencyDataMapper.GetDecimal(_decimalPlace2);

            this.InvoiceNoTextBox.Text = _paymentTradeBL.GetFileNmbrFINAPPostingByInvoiceNo(_finPayTradeDb.InvoiceNo);
            this.CurrCodeTextBox.Text = _finPayTradeDb.CurrCode;
            this.ForexRateTextBox.Text = _finPayTradeDb.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _apInvoice = Convert.ToDecimal((_finPayTradeDb.APInvoice == null) ? 0 : _finPayTradeDb.APInvoice);
            this.APInvoiceTextBox.Text = (_apInvoice == 0) ? "0" : _apInvoice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _apBalance = Convert.ToDecimal((_finPayTradeDb.APBalance == null) ? 0 : _finPayTradeDb.APBalance);
            this.APBalanceTextBox.Text = (_apBalance == 0) ? "0" : _apBalance.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _apPaid = Convert.ToDecimal((_finPayTradeDb.APPaid == null) ? 0 : _finPayTradeDb.APPaid);
            this.APPaidTextBox.Text = (_apPaid == 0) ? "0" : _apPaid.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _ppnRate = Convert.ToDecimal((_finPayTradeDb.PPNRate == null) ? 0 : _finPayTradeDb.PPNRate);
            this.PPNRateTextBox.Text = (_ppnRate == 0) ? "0" : _ppnRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _ppnInvoice = Convert.ToDecimal((_finPayTradeDb.PPNInvoice == null) ? 0 : _finPayTradeDb.PPNInvoice);
            this.PPNInvoiceTextBox.Text = (_ppnInvoice == 0) ? "0" : _ppnInvoice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _ppnBalance = Convert.ToDecimal((_finPayTradeDb.PPNBalance == null) ? 0 : _finPayTradeDb.PPNBalance);
            this.PPNBalanceTextBox.Text = (_ppnBalance == 0) ? "0" : _ppnBalance.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _ppnPaid = Convert.ToDecimal((_finPayTradeDb.PPNPaid == null) ? 0 : _finPayTradeDb.PPNPaid);
            this.PPNPaidTextBox.Text = (_ppnPaid == 0) ? "0" : _ppnPaid.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountForexTextBox.Text = (_finPayTradeDb.AmountForex == 0) ? "0" : _finPayTradeDb.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountInvoiceTextBox.Text = (_finPayTradeDb.AmountInvoice == 0) ? "0" : _finPayTradeDb.AmountInvoice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountBalanceTextBox.Text = (_finPayTradeDb.AmountBalance == 0) ? "0" : _finPayTradeDb.AmountBalance.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _amountHome = Convert.ToDecimal((_finPayTradeDb.AmountHome == null) ? 0 : _finPayTradeDb.AmountHome);
            this.AmountHomeTextBox.Text = (_amountHome == 0) ? "0" : _amountHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace2));
            this.RemarkTextBox.Text = _finPayTradeDb.Remark;
            decimal _value = Convert.ToDecimal((_finPayTradeDb.FgValue == null) ? 0 : _finPayTradeDb.FgValue);
            this.FgValueTextBox.Text = (_value == 0) ? "0" : _value.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINPayTradeDb _finPayTradeDb = _paymentTradeBL.GetSingleFINPayTradeDb(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            _finPayTradeDb.APInvoice = Convert.ToDecimal((this.APInvoiceTextBox.Text == "") ? "0" : this.APInvoiceTextBox.Text);
            _finPayTradeDb.APBalance = Convert.ToDecimal((this.APBalanceTextBox.Text == "") ? "0" : this.APBalanceTextBox.Text);
            _finPayTradeDb.APPaid = Convert.ToDecimal((this.APPaidTextBox.Text == "") ? "0" : this.APPaidTextBox.Text);
            _finPayTradeDb.PPNRate = Convert.ToDecimal((this.PPNRateTextBox.Text == "") ? "0" : this.PPNRateTextBox.Text);
            _finPayTradeDb.PPNInvoice = Convert.ToDecimal((this.PPNInvoiceTextBox.Text == "") ? "0" : this.PPNInvoiceTextBox.Text);
            _finPayTradeDb.PPNBalance = Convert.ToDecimal((this.PPNBalanceTextBox.Text == "") ? "0" : this.PPNBalanceTextBox.Text);
            _finPayTradeDb.PPNPaid = Convert.ToDecimal((this.PPNPaidTextBox.Text == "") ? "0" : this.PPNPaidTextBox.Text);
            _finPayTradeDb.AmountForex = Convert.ToDecimal((this.AmountForexTextBox.Text == "") ? "0" : this.AmountForexTextBox.Text);
            _finPayTradeDb.AmountInvoice = Convert.ToDecimal((this.AmountInvoiceTextBox.Text == "") ? "0" : this.AmountInvoiceTextBox.Text);
            _finPayTradeDb.AmountBalance = Convert.ToDecimal((this.AmountBalanceTextBox.Text == "") ? "0" : this.AmountBalanceTextBox.Text);
            _finPayTradeDb.AmountHome = Convert.ToDecimal((this.AmountHomeTextBox.Text == "") ? "0" : this.AmountHomeTextBox.Text);
            _finPayTradeDb.Remark = this.RemarkTextBox.Text;
            _finPayTradeDb.FgValue = Convert.ToDecimal(this.FgValueTextBox.Text);

            bool _result = this._paymentTradeBL.EditFINPayTradeDb(_finPayTradeDb);

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