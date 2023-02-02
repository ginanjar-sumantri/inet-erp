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

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentTrade
{
    public partial class PaymentTradeDbView : PaymentTradeBase
    {
        private PaymentTradeBL _paymentTradeBL = new PaymentTradeBL();
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
            FINPayTradeDb _finPayTradeDb = this._paymentTradeBL.GetSingleFINPayTradeDb(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currBL.GetDecimalPlace(_finPayTradeDb.CurrCode);
            byte _decimalPlace2 = _currBL.GetDecimalPlace(_currBL.GetCurrDefault());
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