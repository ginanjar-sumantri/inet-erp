using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPSupplierRetur
{
    public partial class DPSuppReturPaymentView : DPSupplierReturBase
    {
        private PaymentBL _paymentBL = new PaymentBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private BankBL _bankBL = new BankBL();
        private FINDPSuppReturBL _finDPSuppReturBL = new FINDPSuppReturBL();
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

                this.ClearLabel();
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

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            FINDPSuppReturPay _finDPSuppReturPay = this._finDPSuppReturBL.GetSingleFINDPSuppReturPay(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finDPSuppReturPay.CurrCode);
            byte _decimalPlaceHome = this._currencyBL.GetDecimalPlace(this._currencyBL.GetCurrDefault());

            this.ReceiptTypeTextBox.Text = this._paymentBL.GetPaymentName(_finDPSuppReturPay.ReceiptType);
            this.DocumentNoTextBox.Text = _finDPSuppReturPay.DocumentNo;
            this.CurrCodeTextBox.Text = _finDPSuppReturPay.CurrCode;
            this.CurrRateTextBox.Text = (_finDPSuppReturPay.ForexRate == 0) ? "0" : _finDPSuppReturPay.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountForexTextBox.Text = (_finDPSuppReturPay.AmountForex == 0) ? "0" : _finDPSuppReturPay.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _tempAmountHome = Convert.ToDecimal((_finDPSuppReturPay.AmountHome == null) ? 0 : _finDPSuppReturPay.AmountHome);
            this.AmountHomeTextBox.Text = (_tempAmountHome == 0) ? "0" : _tempAmountHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
            this.RemarkTextBox.Text = _finDPSuppReturPay.Remark;
            this.BankGiroTextBox.Text = this._bankBL.GetBankNameByCode(_finDPSuppReturPay.BankGiro);

            if (_finDPSuppReturPay.DueDate != null)
            {
                this.DateTextBox.Text = DateFormMapper.GetValue(_finDPSuppReturPay.DueDate);
            }

            if (_finDPSuppReturPay.BankExpense != null)
            {
                decimal _tempBankExpense = Convert.ToInt32(_finDPSuppReturPay.BankExpense);
                this.BankExpenseTextBox.Text = (_tempBankExpense == 0) ? "0" : _tempBankExpense.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }

            char _status = this._finDPSuppReturBL.GetStatusFINDPSuppReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_status == DPSupplierReturDataMapper.GetStatus(TransStatus.Posted))
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
            Response.Redirect(this._editDetailPage2 + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeItemKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeItemKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}